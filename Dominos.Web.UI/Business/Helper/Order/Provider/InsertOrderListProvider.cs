using Dominos.Common.Classes;
using Dominos.Common.Helpers;
using Dominos.Web.UI.Models;
using System.Linq;
using Dominos.Common.Enums.Notification;
using Dominos.Common.Constants;

namespace Dominos.Web.UI.Business.Helper.Order.Provider
{
    public class InsertOrderListProvider : BaseProvider, IProvider<ViewModel>
    {
        public void Execute(ViewModel model)
        {
            if (model.OrderList.Any(x => x.Id == model.Product.Id))
            {
                foreach (var item in model.OrderList)
                {
                    if (item.Id == model.Product.Id)
                    {
                        if (model.Product.Count > 0 )
                        {
                            item.Count += model.Product.Count;
                            AddValidationMessage(model,"İşlem başarılı. ", NotificationTypes.success);
                        }
                        else
                        {
                            AddValidationMessage(model, "Eklemek istediğiniz değer sıfıdan küçük olamaz. ", NotificationTypes.warning);
                        }
                        break;
                    }
                }
            }
            else
            {
                var result = HttpHelper.Post<ServiceResult<OrderTemplate>, ProductTemplate>(model.Product, $""+ApiConstants.GetProduct, "application/json");

                if (!result.HasError)
                {
                    model.OrderList.Add(result.Result);
                    AddValidationMessage(model, "İşlem başarılı. ", NotificationTypes.success);
                }
                else
                {
                    AddValidationMessage(model, "İşlem başarısız. ", NotificationTypes.error);
                }
            }
            GetProductList(model);
            CalculateOrder(model);
        }
    }
}