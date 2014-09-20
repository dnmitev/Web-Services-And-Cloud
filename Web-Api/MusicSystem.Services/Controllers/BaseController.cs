namespace MusicSystem.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using MusicSystem.Data.Contracts;

    public class BaseController : ApiController
    {
        private IMusicSystemData data;

        protected BaseController(IMusicSystemData data)
        {
            this.Data = data;
        }

        public IMusicSystemData Data
        {
            get
            {
                return this.data;
            }

            set
            {
                this.data = value;
            }
        }
    }
}