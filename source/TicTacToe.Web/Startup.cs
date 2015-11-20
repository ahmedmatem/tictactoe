using System.Web.Http;
using System.Reflection;

using Microsoft.Owin;
using Owin;

using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Ninject;
using TicTacToe.Data;

[assembly: OwinStartup(typeof(TicTacToe.Web.Startup))]

namespace TicTacToe.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(GlobalConfiguration.Configuration);
        }

        //private static StandardKernel CreateKernel()
        //{
        //    var kernel = new StandardKernel();
        //    kernel.Load(Assembly.GetExecutingAssembly());

        //    RegisterMappings(kernel);

        //    return kernel;
        //}

        //private static void RegisterMappings(StandardKernel kernel)
        //{
        //    kernel
        //        .Bind<ITicTacToeData>()
        //        .To<TicTacToeData>()
        //        .WithConstructorArgument("context", c => new TicTacToeDbContext());
        //}
    }
}
