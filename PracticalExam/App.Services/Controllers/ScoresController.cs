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

    [RoutePrefix("api/scores")]
    public class ScoresController : BaseApiController
    {
        private const int DefaultHighScoreBoardPlayersNumber = 10;

        // TODO: Uncomment to use without Ninject if there are problems
        //public ScoresController()
        //    : this(new AppData())
        //{
        //}

        public ScoresController(IAppData data)
            : base(data)
        {
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult All()
        {
            var users = this.Data.Users
                            .All()
                            .Select(ScoreDataModel.FromScore)
                            .OrderByDescending(u => u.Rank)
                            .ThenBy(u => u.Username)
                            .Take(DefaultHighScoreBoardPlayersNumber);

            return Ok(users);
        }
    }
}