namespace App.Services.DataModels
{
    using App.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    public class GuessDataModel
    {
        public GuessDataModel()
        {

        }

        public GuessDataModel(Guess guess)
        {
            this.Id = guess.Id;
            this.UserId = guess.UserId;
            this.Username = guess.Username;
            this.GameId = guess.GameId;
            this.Number = guess.Number;
            this.DateMade = guess.DateMade;
            this.BullsCount = guess.BullsCount;
            this.CowsCount = guess.CowsCount;
        }

        public static Expression<Func<Guess, GuessDataModel>> FromGuess
        {
            get
            {
                return g => new GuessDataModel
                {
                    Id = g.Id,
                    UserId = g.User.Id,
                    Username = g.Username,
                    GameId = g.Game.Id,
                    Number = g.Number,
                    DateMade = g.DateMade,
                    BullsCount = g.BullsCount,
                    CowsCount = g.CowsCount
                };
            }
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public int GameId { get; set; }

        [Required]
        [Range(1000, 9999)]
        public int Number { get; set; }

        public DateTime DateMade { get; set; }

        public int BullsCount { get; set; }

        public int CowsCount { get; set; }
    }
}