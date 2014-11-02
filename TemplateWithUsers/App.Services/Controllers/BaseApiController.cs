namespace App.Services.Controllers
{
    using System.Web.Http;

    using App.Data.Contracts;

    public abstract class BaseApiController : ApiController
    {
        public BaseApiController(IAppData data)
        {
            this.Data = data;
        }

        public IAppData Data { get; private set; }
    }
}