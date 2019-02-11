using Dominos.Common.Classes;
using Dominos.Common.Constants;
using Dominos.Common.Enums;
using Dominos.Common.Enums.Notification;
using Dominos.Common.Helpers;
using Dominos.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominos.Web.UI.Business.Helper
{
    public class BaseProvider
    {
        public void GetProductList(ViewModel model)
        {
            var result = HttpHelper.Get<ServiceResult<List<ProductTemplate>>>($""+ApiConstants.GetProductList);

            if (!result.HasError)
            {
                model.ProductList = result.Result;
            }
            else
            {
                AddValidationMessage(model, "Hata meydana geldi!!! ", NotificationTypes.error);
            }
        }

        public void CalculateOrder(ViewModel model)
        {
            if (model.OrderList.Any())
            {
                var totalCriteria = 0.0;
                foreach (var item in model.OrderList)
                {
                    if (item.ProductTypeId != (int)ProductTypeEnum.Drink)
                    {
                        totalCriteria += Convert.ToDouble(item.Price) * item.Count;
                    }
                    model.OrderDetail.TotalDiscountPrice += (Convert.ToDouble(item.DiscountPrice) * item.Count);
                    model.OrderDetail.TotalPrice += Convert.ToDouble(item.Price) * item.Count;
                }

                if (totalCriteria > 100)
                {
                    model.OrderDetail.OrderTotalPrice = model.OrderDetail.TotalDiscountPrice;
                    model.OrderDetail.TotalDiscount = model.OrderDetail.TotalPrice - model.OrderDetail.TotalDiscountPrice;
                }
                else
                {
                    model.OrderDetail.OrderTotalPrice = model.OrderDetail.TotalPrice;
                }
            }
        }

        public void AddValidationMessage(ViewModel model, string validationMessage, NotificationTypes type)
        {
            model.ValidationMessages += validationMessage;
            model.NotificationTypes = type;
        }

    }
}