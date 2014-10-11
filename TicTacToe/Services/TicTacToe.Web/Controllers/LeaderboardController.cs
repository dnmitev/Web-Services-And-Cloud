using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TicTacToe.Data;
using TicTacToe.Web.DataModels;

namespace TicTacToe.Web.Controllers
{
    public class LeaderboardController : BaseApiController
    {
        //public LeaderboardController()
        //    : this(new TicTacToeData(TicTacToeDbContext.Create()))
        //{
        //}

        public LeaderboardController(ITicTacToeData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult GetLeaderboard()
        {
            var result = this.data.Users
                             .All()
                             .Select(LeaderboardDataModel.FromUser)
                             .OrderByDescending(u => u.Score)
                             .ThenBy(u => u.Username);

            return Ok(result);
        }
    }
}