namespace App.Services.DataModels
{
    using App.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class CreateGameResponseDataModel
    {
        //  "Id": 6,
        //  "Name": "The Empire strikes back!",
        //  "Blue": "No blue player yet",
        //  "Red": "dodo@minkov.it",
        //  "GameState": "WaitingForOpponent",
        //  "DateCreated": "2014-09-23T06:41:51.5816277+03:00"

        private const string NotYetJoined = "No blue player yet";

        public CreateGameResponseDataModel(Game game)
        {
            this.Id = game.Id;
            this.Name = game.Name;
            this.Red = game.FirstUser.UserName;
            this.Blue = NotYetJoined;
            this.GameState = game.GameState;
            this.DateCreated = game.DateCreated;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Blue { get; set; }

        public string Red { get; set; }

        public GameState GameState { get; set; }

        public DateTime DateCreated { get; set; }
    }
}