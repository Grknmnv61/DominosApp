using Dominos.Business.Infrastructure;
using Dominos.Business.Order;
using Dominos.Business.Product;
using System.Web.Http;

namespace Dominos.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        public IProductService ProductService
        {
            get
            {
                return IoC.Resolve<IProductService>();
            }
        }

        public IOrderService OrderService
        {
            get
            {
                return IoC.Resolve<IOrderService>();
            }
        }
    }
}
