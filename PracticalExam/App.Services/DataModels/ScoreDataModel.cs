namespace App.Services.DataModels
{
    using System;
    using System.Linq;
    using App.Models;
using System.Linq.Expressions;

    public class ScoreDataModel
    {
        public static Expression<Func<User, ScoreDataModel>> FromScore
        {
            get
            {
                return u => new ScoreDataModel
                {
                    Rank = 100 * u.WinsCount + 15 * u.LossesCount,
                    Username = u.UserName
                };
            }
        }

        public int? Rank { get; set; }

        public string Username { get; set; }
    }
}