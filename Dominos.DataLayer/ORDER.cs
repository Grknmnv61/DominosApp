//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dominos.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class ORDER
    {
        public int Id { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public Nullable<double> DiscountPrice { get; set; }
        public string CustomerAddressDescription { get; set; }
    }
}
