using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Reflection;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;

using App.Data.Contracts;
using App.Data;
using App.GameLogic.Contracts;
using App.GameLogic;
using App.Utilities.Contracts;
using App.Utilities;

[assembly: OwinStartup(typeof(App.Services.Startup))]

namespace App.Services
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(GlobalConfiguration.Configuration);
        }

        //// TODO: Set IoC -> Ninject
        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            BindTypes(kernel);

            return kernel;
        }

        private static void BindTypes(StandardKernel kernel)
        {
            kernel.Bind<IAppData>().To<AppData>().WithConstructorArgument("context", c => new ApplicationDbContext());
            kernel.Bind<INumberValidator>().To<NumberValidator>();
            kernel.Bind<IBullsAndCowsCounter>().To<BullsAndCowsCounter>();
        }
    }
}
