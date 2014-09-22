namespace App.Services.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;

    using App.Data.Contracts;
    
    public class TestBugsDependencyResolver : IDependencyResolver
    {
        private IAppData data;

        public IAppData Data
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

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            // TODO: Implement for specific controller
            //if (serviceType == typeof(BugsController))
            //{
            //    return new BugsController(this.Data);
            //}

            //return null;
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}