namespace Articles.Services.Controllers
{
    using System.Web.Http;

    using Articles.Data;
    using Articles.Data.Contracts;

    public class BaseApiController : ApiController
    {
        private IAppData data;

        //// TODO: IoC -> Ninject
        //public BaseApiController()
        //    : this(new AppData())
        //{
        //}

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