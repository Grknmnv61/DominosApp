using Dominos.Common.Classes;
using Dominos.Common.Classes.Customer;
using Dominos.Common.Classes.Order;
using Dominos.Common.Helpers;
using System.Collections.Generic;

namespace Dominos.Business.Order
{
    public interface IOrderService
    {
        ServiceResult<OrderTemplate> GetProductById(int productId, int count);
        ServiceResult<int> CreateOrder(CustomerTemplate customerTemplate, List<OrderTemplate> orderList, OrderDetailTemplate orderTemplate);
    }
}
