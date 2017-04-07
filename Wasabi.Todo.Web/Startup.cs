using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Wasabi.Todo.Services.Mapping;
using Wasabi.Todo.Web;
using Wasabi.Todo.Web.Core.Mapping;
using Wasabi.Todo.Web.Models;
using Wasabi.Todo.Web.Modules;

[assembly: OwinStartup(typeof(Startup))]

namespace Wasabi.Todo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // Register OAuth
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<UserStore<ApplicationUser>>().As<IUserStore<ApplicationUser>>();
            builder.RegisterType<UserManager<ApplicationUser>>();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();

            // Register WebApi
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            // Register Modules
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EntityFrameworkModule());
            builder.RegisterModule(new LoggingModule());

            builder.RegisterInstance(AutoMapperWebConfig.Mapper)
                .As<IMapper>()
                .SingleInstance();



            // Build the container
            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            // Register with OWIN
            app.UseAutofacMiddleware(container);

            ConfigureAuth(app);
        }
    }
}
