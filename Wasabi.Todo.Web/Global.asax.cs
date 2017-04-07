using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Wasabi.Todo.Services.Mapping;
using Wasabi.Todo.Web.Core.Mapping;

namespace Wasabi.Todo.Web
{
    public class TodoApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Register Mapper
            AutoMapperWebConfig.ConfigureAutoMapper();
            
        }
    }
}
