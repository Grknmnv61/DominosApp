using Dominos.Common.Classes;
using Dominos.Common.Enums.Notification;
using Dominos.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominos.Web.UI.Business.Helper.Order.Provider
{
    public class RemoveOrderListProvider : BaseProvider, IProvider<ViewModel>
    {
        public void Execute(ViewModel model)
        {
            OrderTemplate orderItem = new OrderTemplate();

            foreach (var item in model.OrderList)
            {
                if (item.Id == model.Product.Id)
                {
                    if (item.Count > 1)
                    {
                        item.Count -= 1;
                        AddValidationMessage(model, "Ürün eksiltme işlemi başarıyla gerçekleşti. ", NotificationTypes.success);
                    }
                    else
                    {
                        orderItem = item;
                    }
                    break;
                }
            }

            if (orderItem?.Id == model.Product.Id)
            {
                model.OrderList.Remove(orderItem);
                AddValidationMessage(model, "Silme işlemi başarıyla gerçekleşti. ", NotificationTypes.success);
            }

            GetProductList(model);
            CalculateOrder(model);
        }
    }
}