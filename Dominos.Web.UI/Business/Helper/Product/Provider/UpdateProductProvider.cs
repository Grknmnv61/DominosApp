using Dominos.Common.Classes;
using Dominos.Common.Constants;
using Dominos.Common.Enums.Notification;
using Dominos.Common.Helpers;
using Dominos.Web.UI.Models;
using System;
using System.Linq;

namespace Dominos.Web.UI.Business.Helper.Product.Provider
{
    public class UpdateProductProvider : BaseProvider, IProvider<ViewModel>
    {
        public void Execute(ViewModel model)
        {
            UpdateControl(model);

            if (model.ValidationMessages == null)
            {
                var result = HttpHelper.Post<ServiceResult<bool>, ProductTemplate>(model.Product, $""+ApiConstants.UpdateProduct, "application/json");

                if (!result.HasError)
                {
                    if (result.Result)
                    {
                        model.Result = result.Result;
                        AddValidationMessage(model, "İşlem başarılı. ", NotificationTypes.success);
                    }
                    else
                    {
                        model.Result = result.Result;
                        AddValidationMessage(model, "Güncelleme işlemi başarısız. ", NotificationTypes.error);
                    }
                }
            }
            GetProductList(model);
            UpdateOrderList(model);
        }

        private void UpdateOrderList(ViewModel model)
        {
            if (model.OrderList.Any())
            {
                foreach (var item in model.OrderList)
                {
                    if (item.Id == model.Product.Id)
                    {
                        var updatedProduct = model.ProductList.Where(x => x.Id == model.Product.Id).FirstOrDefault();
                        item.ProductName = updatedProduct.Name;
                        item.Price = Convert.ToString(updatedProduct.Price);
                        CalculateOrder(model);
                        break;
                    }
                }
            }
        }

        private static void UpdateControl(ViewModel model)
        {
            if (model.Product.Name == " " || model.Product.Name == string.Empty || model.Product.Name == null)
            {
                model.ValidationMessages += "Ürün ismi boş girilemez. ";
            }
            if (model.Product.Description == " " || model.Product.Description == string.Empty || model.Product.Description == null)
            {
                model.ValidationMessages += "Ürün acıklaması boş girilemez. ";
            }
            if (model.Product.Price <= 0)
            {
                model.ValidationMessages += "Ürün fiyatı sıfır ve sıfırdan kücük girilemez. ";
            }
            model.NotificationTypes = NotificationTypes.warning;
        }
    }
}