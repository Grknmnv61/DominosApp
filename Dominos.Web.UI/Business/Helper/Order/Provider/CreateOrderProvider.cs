using Dominos.Common.Constants;
using Dominos.Common.Enums.Notification;
using Dominos.Common.Helpers;
using Dominos.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominos.Web.UI.Business.Helper.Order.Provider
{
    public class CreateOrderProvider : BaseProvider, IProvider<ViewModel>
    {
        public void Execute(ViewModel model)
        {
            OrderControl(model);
            CalculateOrder(model);

            if (model.ValidationMessages == null)
            {
                var result = HttpHelper.Post<ServiceResult<int>, ViewModel>(model, $""+ApiConstants.CreateOrder, "application/json");

                if (!result.HasError)
                {
                    if (result.Result > 0)
                    {
                        model.OrderList.Clear();
                        var successMessage = "Sipariş Oluşturma işlemi başarılı. Sipariş Numaranız:" + result.Result;
                        AddValidationMessage(model, successMessage, NotificationTypes.success);
                    }
                    else
                    {
                        AddValidationMessage(model, "Sipariş Oluşturma işlemi başarısız!!! ", NotificationTypes.error);
                    }
                }
            }
            GetProductList(model);
        }

        private void OrderControl(ViewModel model)
        {
            if (model.OrderList.Count <= 0)
            {
                AddValidationMessage(model, "Sipariş listesi oluşmadığında sipariş oluşturalamıyor!!! ", NotificationTypes.warning);
            }

            if (model.Customer.Name == " " || model.Customer.Name == string.Empty || model.Customer.Name == null)
            {
                AddValidationMessage(model, "Sipariş oluşturulurken isim boş girilemez!!! ", NotificationTypes.warning);
            }

            if (model.Customer.Adress == " " || model.Customer.Adress == string.Empty || model.Customer.Adress == null)
            {
                AddValidationMessage(model, "Sipariş oluşturulurken adres boş girilemez!!! ", NotificationTypes.warning);
            }
        }
    }
}