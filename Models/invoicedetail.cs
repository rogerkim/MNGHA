//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Homework.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class invoicedetail
    {
        public int invoicedetailid { get; set; }
        public string itemcode { get; set; }
        public Nullable<int> quantity { get; set; }
        public int invoiceid { get; set; }
        public Nullable<System.DateTime> adddate { get; set; }
        public Nullable<System.DateTime> editdate { get; set; }
        public Nullable<decimal> price { get; set; }
    
        public virtual invoice invoice { get; set; }
        public virtual item item { get; set; }
    }
}