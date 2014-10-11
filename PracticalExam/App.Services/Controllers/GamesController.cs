namespace App.Services.Controllers
{
    using App.Data;
    using App.Data.Contracts;
    using App.Services.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using App.Models;
    using App.GameLogic.Contracts;
    using App.GameLogic;
    using App.Utilities.Contracts;
    using App.Utilities;

    [RoutePrefix("api/games")]
    public class GamesController : BaseApiController
    {
        private const string SuccessfullJoin = "You joined game \"{0}\"";
        private const int DefaultPageSize = 10;

        private readonly INumberValidator numberValidator;
        private readonly IBullsAndCowsCounter bullsAndCowsCounter;
        private readonly IRandomProvider randomProvider;

        public GamesController()
            : this(new AppData(), new NumberValidator(), new BullsAndCowsCounter(), RandomProvider.Instance)
        {
        }

        public GamesController(IAppData data)
            : base(data)
        {
        }

        public GamesController(IAppData data, INumberValidator validator, IBullsAndCowsCounter bullsAndCowsCounter, IRandomProvider randomProvider)
            : base(data)
        {
            this.numberValidator = validator;
            this.bullsAndCowsCounter = bullsAndCowsCounter;
            this.randomProvider = randomProvider;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult All()
        {
            return this.All(0);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult All(int page)
        {
            var games = this.GetGames(page)
                            .Where(g => g.GameState == GameState.WaitingForOpponent)
                            .Select(GetGameDataModel.FromGame);

            return Ok(games);
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public IHttpActionResult AllForPlayer()
        {
            return this.All(0);
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public IHttpActionResult AllForPlayer(int page)
        {
            var userId = this.GetCurrentUserId();

            var games = this.GetGames(page)
                            .Where(g => g.GameState == GameState.WaitingForOpponent || g.FirstUser.Id == userId || g.SecondUser.Id == userId)
                            .Select(GetGameDataModel.FromGame);

            return Ok(games);
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public IHttpActionResult GetGameDetails(int id)
        {
            var game = this.Data.Games
                           .All()
                           .Where(g => g.GameState != GameState.WaitingForOpponent && g.GameState != GameState.Finished)
                           .FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            var currentUserId = this.GetCurrentUserId();

            var currentUserSecretNumber = game.FirstUserId == currentUserId ? game.FirstUserSecretNumber : game.SecondUserSecretNumber;
            var currentUserColor = game.FirstUserId == currentUserId ? "Red" : "Blue";
            var currentUserGuesses = this.Data.Guesses
                                         .All()
                                         .Where(guess => guess.UserId == currentUserId)
                                         .Select(GuessDataModel.FromGuess)
                                         .ToList();

            var opponentGuess = this.Data.Guesses
                                    .All()
                                    .Where(guess => guess.UserId != currentUserId)
                                    .Select(GuessDataModel.FromGuess)
                                    .ToList();

            var gameDetails = new GameDetailsDataModel(game);

            gameDetails.YourNumber = currentUserSecretNumber;
            gameDetails.YourGuesses = currentUserGuesses;
            gameDetails.OpponentGuesses = opponentGuess;
            gameDetails.YourColor = currentUserColor;

            return Ok(gameDetails);
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/guess")]
        public IHttpActionResult MakeGuess(int id, MakeGuessModel guess)
        {
            var game = this.Data.Games.All().FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            if (game.GameState == GameState.Finished)
            {
                return BadRequest("Game has already finished");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!this.IsValidNumber(guess.Number))
            {
                return BadRequest("Invalid guess number");
            }

            var currentUserId = this.GetCurrentUserId();
            var currentUser = this.GetCurrentUser(currentUserId);

            var secretNumberToGuess = game.SecondUserId == currentUserId ? game.FirstUserSecretNumber : game.SecondUserSecretNumber;
            var currentUserColor = game.FirstUserId == currentUserId ? "Red" : "Blue";

            if (currentUserColor == "Red")
            {
                if (game.GameState != GameState.RedInTurn)
                {
                    return BadRequest("Blue is in turn");
                }
            }

            if (currentUserColor == "Blue")
            {
                if (game.GameState != GameState.BlueInTurn)
                {
                    return BadRequest("Red is in turn");
                }
            }

            var result = this.GetBullsAndCows(secretNumberToGuess, guess.Number);

            var newGuess = new Guess
            {
                UserId = currentUserId,
                Username = currentUser.UserName,
                GameId = id,
                Number = guess.Number,
                DateMade = DateTime.Now,
                CowsCount = result.CowsCount,
                BullsCount = result.BullsCount
            };

            this.Data.Guesses.Add(newGuess);
            this.Data.SaveChanges();

            // switch players turn
            game.GameState = currentUserColor == "Red" ? GameState.BlueInTurn : GameState.RedInTurn;

            // 4 bulls -> game finished
            if (newGuess.BullsCount == 4)
            {
                game.GameState = GameState.Finished;

                var winner = this.Data.Users.All().FirstOrDefault(u => u.Id == currentUserId);
                winner.WinsCount++;

                var loser = this.Data.Users.All().FirstOrDefault(u => u.Id != currentUserId);
                loser.LossesCount++;

                this.Data.Users.Update(winner);
                this.Data.Users.Update(loser);
                this.Data.SaveChanges();
            }

            this.Data.Games.Update(game);
            this.Data.SaveChanges();


            var guessDetails = new GuessDataModel(newGuess);

            return Ok(guessDetails);
        }

        private IBullsAndCows GetBullsAndCows(int? secretNumberToGuess, int guessNumber)
        {
            return this.bullsAndCowsCounter.CountBullsAndCows(secretNumberToGuess, guessNumber);
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateGame(GameCreateDataModel gameModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!this.IsValidNumber(gameModel.Number))
            {
                return BadRequest("Number is not valid");
            }

            var userId = GetCurrentUserId();

            var user = GetCurrentUser(userId);

            var game = new Game
            {
                Name = gameModel.Name,
                GameState = GameState.WaitingForOpponent,
                DateCreated = DateTime.Now,
                FirstUserId = userId,
                FirstUser = user,
                FirstUserSecretNumber = gameModel.Number
            };

            this.Data.Games.Add(game);
            this.Data.SaveChanges();

            var gameDataToReturn = new CreateGameResponseDataModel(game);

            return Created(string.Format("api/games/{0}", gameDataToReturn.Id), gameDataToReturn);
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult JoinGame(int id, JoinGameDataModel gameModel)
        {
            var gameToJoin = this.Data.Games
                                 .All()
                                 .FirstOrDefault(g => g.Id == id);

            if (gameToJoin == null)
            {
                return NotFound();
            }

            if (!this.IsValidNumber(gameModel.Number))
            {
                return BadRequest("Number is not valid");
            }

            var userId = this.GetCurrentUserId();
            var user = this.GetCurrentUser(userId);

            gameToJoin.SecondUserSecretNumber = gameModel.Number;
            gameToJoin.SecondUserId = userId;
            gameToJoin.SecondUser = user;

            // randomly decide which player is in turn
            // TODO: Provide IRandomProvider if you have time
            gameToJoin.GameState = randomProvider.GetRandomInt(0, 1) == 0 ? GameState.BlueInTurn : GameState.RedInTurn;

            this.Data.Games.Update(gameToJoin);
            this.Data.SaveChanges();

            return Ok(new { result = string.Format(SuccessfullJoin, gameToJoin.Name) });
        }

        private IQueryable<Game> GetGames(int page)
        {
            return this.Data.Games
                       .All()
                       .OrderBy(g => g.GameState)
                       .ThenBy(g => g.Name)
                       .ThenBy(g => g.DateCreated)
                       .ThenBy(g => g.FirstUser.UserName)
                       .Skip(DefaultPageSize * page)
                       .Take(DefaultPageSize);
        }

        private string GetCurrentUserId()
        {
            var userId = this.User.Identity.GetUserId();
            return userId;
        }

        private User GetCurrentUser(string userId)
        {
            var user = this.Data.Users
                           .All()
                           .FirstOrDefault(u => u.Id == userId);

            return user;
        }

        private bool IsValidNumber(int number)
        {
            return this.numberValidator.IsNumberValid(number);
        }
    }
}