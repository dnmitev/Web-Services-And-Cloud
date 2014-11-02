namespace App.Services.Controllers
{
    using System.Web.Http;
    
    using App.Data;
    using App.Data.Contracts;

    public class BaseApiController : ApiController
    {
        public BaseApiController(IAppData data)
        {
            this.Data = data;
        }

        public IAppData Data { get; private set; }
    }
}