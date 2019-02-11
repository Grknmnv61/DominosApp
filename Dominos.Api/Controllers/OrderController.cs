using Dominos.Api.Helper;
using Dominos.Business.Order;
using Dominos.Common.Classes;
using Dominos.Web.UI.Models;
using System.Net.Http;
using System.Web.Http;

namespace Dominos.Api.Controllers
{
    [RoutePrefix("api/order")]
    public class OrderController : BaseApiController
    {
        [Route("GetProduct")]
        [HttpPost]
        public HttpResponseMessage GetProduct(ProductTemplate product)
        {
            var result = OrderService.GetProductById(product.Id,product.Count);
            return ControllerHelper.GetJsonResponseMessage(result);
        }

        [Route("CreateOrder")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(ViewModel model)
        {
            var result = OrderService.CreateOrder(model.Customer,model.OrderList,model.OrderDetail);
            return ControllerHelper.GetJsonResponseMessage(result);
        }
    }
}
