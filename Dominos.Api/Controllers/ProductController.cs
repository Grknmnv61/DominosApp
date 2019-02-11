using Dominos.Api.Helper;
using Dominos.Common.Classes;
using System.Net.Http;
using System.Web.Http;

namespace Dominos.Api.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : BaseApiController
    {
        [Route("GetProductList")]
        [HttpGet]
        public HttpResponseMessage GetProductList()
        {
            var result = ProductService.GetAllProductByRedis();
            return ControllerHelper.GetJsonResponseMessage(result);
        }

        [Route("UpdateProduct")]
        [HttpPost]
        public HttpResponseMessage UpdateProduct(ProductTemplate product)
        {
            var result = ProductService.UpdateProduct(product);
            return ControllerHelper.GetJsonResponseMessage(result);
        }

    }
}
