using Autofac.Integration.Mvc;
using Dominos.Business.Infrastructure;
using Dominos.Business.Product;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dominos.Web.UI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(IoC.CreateContainer()));
            IoC.Resolve<IProductService>().DatabaseInitializeFactory("Redis");
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
