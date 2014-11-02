namespace App.Services.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    using App.Data;
    using App.Data.Contracts;

    // TODO: Set service route
    // [RoutePrefix("api/games")]
    public class MyController : BaseApiController
    {
        // An empty constructor is needed to work properly.
        // The Poor man's DI can be switched with the use of Ninject or other IoC Container
        public MyController()
            : this(new AppData())
        {
        }

        public MyController(IAppData data)
            : base(data)
        {
        }

        // TODO: Fix concrete method's route
        [HttpGet]
        [Route("")]
        public IHttpActionResult All()
        {
            var data = this.Data.Models.All();
            return Ok(data);
        }
    }
}