namespace App.Services.Controllers
{
    using System.Web.Http;
    
    using App.Data.Contracts;

    public class BaseApiController : ApiController
    {
        private IAppData data;

        public BaseApiController(IAppData data)
        {
            this.Data = data;
        }

        public IAppData Data
        {
            get
            {
                return this.data;
            }

            private set
            {
                this.data = value;
            }
        }
    }
}