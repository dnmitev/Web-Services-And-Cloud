namespace App.Services.Controllers
{
    using App.Data;
    using App.Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    // TODO: Check controller's route
    [RoutePrefix("myctrl")]
    public class MyController : BaseApiController
    {
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