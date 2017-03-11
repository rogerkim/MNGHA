using Homework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.VM
{
    
    public class InvoiceVM
    {
        // Header
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string InvoiceID { get; set; }
        public string Currency { get; set; }
        public decimal Total { get; set; }
        public string AddDate { get; set; }

        // Detail
        public List<InvoiceDetailVM> Detail { get; set; }
        
        public InvoiceVM()
        {
            Detail = new List<InvoiceDetailVM>();
        }
    }

    public class InvoiceDetailVM
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}