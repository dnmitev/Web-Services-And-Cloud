namespace App.Services.DataModels
{
    using App.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    public class GetGameDataModel
    {
        // "Id": 5,
        // "Name": "Battle of the titans",
        // "Blue": "No blue player yet",
        // "Red": "doncho@minkov.it",
        // "GameState": "WaitingForOpponent",
        // "DateCreated": "2014-09-22T14:31:51.067"
        public static Expression<Func<Game, GetGameDataModel>> FromGame
        {
            get
            {
                return g => new GetGameDataModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    Blue = g.SecondUser.UserName,
                    Red = g.FirstUser.UserName,
                    GameState = g.GameState.ToString(),
                    DateCreated = g.DateCreated
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Blue { get; set; }

        public string Red { get; set; }

        public string GameState { get; set; }

        public DateTime DateCreated { get; set; }
    }
}