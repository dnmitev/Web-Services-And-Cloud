namespace App.Services.DataModels
{
    using App.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class GameDetailsDataModel
    {
        public GameDetailsDataModel(Game game)
        {
            this.Id = game.Id;
            this.Name = game.Name;
            this.DateCreated = game.DateCreated;
            this.Red = game.FirstUser.UserName;
            this.Blue = game.SecondUser.UserName;
            this.GameState = game.GameState.ToString();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public string Red { get; set; }

        public string Blue { get; set; }

        public int? YourNumber { get; set; }

        public ICollection<GuessDataModel> YourGuesses { get; set; }

        public ICollection<GuessDataModel> OpponentGuesses { get; set; }

        public string YourColor { get; set; }

        public string GameState { get; set; }
    }
}