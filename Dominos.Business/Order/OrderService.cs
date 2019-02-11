using Dominos.Common.Classes;
using Dominos.Common.Classes.Customer;
using Dominos.Common.Classes.Order;
using Dominos.Common.Helpers;
using Dominos.DataLayer;
using Dominos.Repository.Repository;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Dominos.Business.Order
{
    public class OrderService : BaseService<DominosDBEntities>, IOrderService
    {
        public OrderService(DBRepository repository) : base(repository)
        {

        }

        public ServiceResult<OrderTemplate> GetProductById(int productId, int count)
        {
            var serviceResult = new ServiceResult<OrderTemplate>();

            try
            {
                var product = (from p in Repository.Query<PRODUCT>().Where(x => x.Id == productId)
                                        join pt in Repository.Query<PRODUCT_TYPE>() on p.ProductTypeId equals pt.Id
                                        select new OrderTemplate
                                        {
                                            Id = p.Id,
                                            ProductName = p.Name,
                                            PriceDouble = p.Price.Value,
                                            DiscountPriceDouble = p.DiscountPrice.Value,
                                            ProductTypeId = pt.Id,
                                            ProductTypeName = pt.Name,
                                            Count = count
                                        }).FirstOrDefault();

                product.Price = Convert.ToString(product.PriceDouble);
                product.DiscountPrice = Convert.ToString(product.DiscountPriceDouble);
                serviceResult.Result = product;
            }
            catch (Exception ex)
            {
                serviceResult.Exception = ex;
                serviceResult.HasError = true;
            }
            return serviceResult;
        }

        public ServiceResult<int> CreateOrder(CustomerTemplate customerTemplate, List<OrderTemplate> orderList, OrderDetailTemplate orderTemplate)
        {
            var serviceResult = new ServiceResult<int>();

            try
            {
                CUSTOMER_ADDRESS customerAdress = AddCustomerAdress(customerTemplate);
                Repository.Commit();

                CUSTOMER customer = AddCustomer(customerTemplate, customerAdress);
                Repository.Commit();

                ORDER order = AddOrder(orderTemplate, customerAdress, customer);
                Repository.Commit();

                foreach (var item in orderList)
                {
                    if (item.Count > 1)
                    {
                        for (int i = 0; i < item.Count; i++)
                        {
                            AddOrderItem(order, item);
                        }
                    }
                    else
                    {
                        AddOrderItem(order, item);
                    }
                }

                Repository.Commit();
                serviceResult.Result = order.Id;
            }
            catch (Exception ex)
            {
                serviceResult.Exception = ex;
                serviceResult.HasError = true;
            }
            return serviceResult;
        }

        private void AddOrderItem(ORDER order, OrderTemplate item)
        {
            ORDER_ITEMS orderItem = new ORDER_ITEMS
            {
                OrderId = order.Id,
                ProductId = item.Id,
                ProductPrice = Convert.ToDouble(item.Price),
                ProductDiscountPrice = Convert.ToDouble(item.DiscountPrice),
                DiscountValue = Convert.ToDouble(item.Price) - Convert.ToDouble(item.DiscountPrice)
            };
            Repository.Context.ORDER_ITEMS.Add(orderItem);
        }

        private ORDER AddOrder(OrderDetailTemplate orderTemplate, CUSTOMER_ADDRESS customerAdress, CUSTOMER customer)
        {
            ORDER order = new ORDER
            {
                CustomerId = customer.Id,
                TotalPrice = orderTemplate.TotalPrice,
                DiscountPrice = orderTemplate.OrderTotalPrice,
                CustomerAddressDescription = customerAdress.Description
            };

            Repository.Context.ORDER.Add(order);
            return order;
        }

        private CUSTOMER AddCustomer(CustomerTemplate customerTemplate, CUSTOMER_ADDRESS customerAdress)
        {
            CUSTOMER customer = new CUSTOMER
            {
                Name = customerTemplate.Name,
                CustomerAdressId = customerAdress.Id
            };

            Repository.Context.CUSTOMER.Add(customer);
            return customer;
        }

        private CUSTOMER_ADDRESS AddCustomerAdress(CustomerTemplate customerTemplate)
        {
            CUSTOMER_ADDRESS customerAdress = new CUSTOMER_ADDRESS
            {
                Description = customerTemplate.Adress
            };

            Repository.Context.CUSTOMER_ADDRESS.Add(customerAdress);
            return customerAdress;
        }
    }
}
