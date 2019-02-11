using Autofac.Integration.Mvc;
using Dominos.Business.Infrastructure;
using System.Web.Http;
using System.Web.Mvc;

namespace Dominos.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(IoC.CreateContainer()));
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
