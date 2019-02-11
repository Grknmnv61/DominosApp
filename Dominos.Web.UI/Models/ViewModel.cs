using Dominos.Common.Classes;
using Dominos.Common.Classes.Customer;
using Dominos.Common.Classes.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominos.Web.UI.Models
{
    public class ViewModel : BaseModel
    {
        public List<ProductTemplate> ProductList { get; set; } = new List<ProductTemplate>();
        public List<OrderTemplate> OrderList { get; set; } = new List<OrderTemplate>();
        public ProductTemplate Product { get; set; } = new ProductTemplate();
        public OrderTemplate Order { get; set; } = new OrderTemplate();
        public OrderDetailTemplate OrderDetail { get; set; } = new OrderDetailTemplate();
        public CustomerTemplate Customer { get; set; } = new CustomerTemplate();
    }
}