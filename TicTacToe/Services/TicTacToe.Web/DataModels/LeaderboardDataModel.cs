using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TicTacToe.Models;

namespace TicTacToe.Web.DataModels
{
    public class LeaderboardDataModel
    {
        public static Expression<Func<User, LeaderboardDataModel>> FromUser
        {
            get
            {
                return u => new LeaderboardDataModel
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Score = (u.Wins * 100 + u.Losses * 10) > 0 ? (u.Wins * 100 + u.Losses * 10) : 0
                };
            }
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public int? Score { get; set; }
    }
}