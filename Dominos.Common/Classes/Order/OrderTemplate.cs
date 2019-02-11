using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominos.Common.Classes
{
    public class OrderTemplate
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public int Count { get; set; }
        public double PriceDouble { get; set; }
        public double DiscountPriceDouble { get; set; }
    }
}
