//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pharmacy.Models.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order_details
    {
        public int id { get; set; }
        public int order_id { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public int medicine_id { get; set; }
    
        public virtual Medicine Medicine { get; set; }
        public virtual Order Order { get; set; }
    }
}
