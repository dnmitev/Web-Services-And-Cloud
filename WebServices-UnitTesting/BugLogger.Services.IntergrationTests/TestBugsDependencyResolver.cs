namespace BugLogger.Services.IntergrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;

    using BugLogger.Data;
    using BugLogger.Data.Contracts;
    using BugLogger.Services.Controllers;
    
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
            if (serviceType == typeof(BugsController))
            {
                return new BugsController(this.Data);
            }

            return null;
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