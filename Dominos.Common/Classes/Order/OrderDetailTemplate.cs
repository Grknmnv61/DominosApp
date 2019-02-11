using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominos.Common.Classes.Order
{
    public class OrderDetailTemplate
    {
        public double TotalPrice { get; set; } 
        public double TotalDiscountPrice { get; set; }
        public double OrderTotalPrice { get; set; }
        public double TotalDiscount { get; set; }
    }
}
