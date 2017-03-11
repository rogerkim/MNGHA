using Homework.Models;
using Homework.VM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Homework.Controllers
{
    public class Section3Controller : Controller
    {
        private HWEntities db = new HWEntities();

        public static string XML_DISPLAY { get; set; }
        public static List<CustomerVM> CustomerVMList { get; set; }

        // GET: Section3
        public ActionResult Index()
        {
            var result = new List<CustomerVM>();
            foreach (var item in db.Customer_Sample.ToList())
            {
                var customer = new CustomerVM();
                customer.Name = item.Name;
                customer.Address = item.Address;
                customer.Category = item.Category;
                customer.Fax = item.Fax;
                customer.Mobile = item.Mobile;
                customer.Telephone = item.Telephone;
                customer.Type = item.Type;
                result.Add(customer);
            }
            return View(result);
        }

        [HttpPost]
        public void ExportToXML(List<CustomerVM> customerList)
        {
            CustomerVMList = customerList;
        }

        [HttpGet]
        public ActionResult XML()
        {
            var xml = new XElement("Customers",
                      from customer in CustomerVMList
                      select new XElement("Customer",
                                   new XAttribute("Name", customer.Name),
                                     new XElement("Type", customer.Type),
                                     new XElement("Category", customer.Category),
                                     new XElement("Address", customer.Address),
                                     new XElement("Telephone", customer.Telephone),
                                     new XElement("Mobile", customer.Mobile),
                                     new XElement("Fax", customer.Fax)
                                 ));

            return Content(xml.ToString(), "text/xml");
        }

        [HttpGet]
        public void ExportToExcel()
        {
            var result = CustomerVMList;

            var dt = new System.Data.DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("Telephone", typeof(string));
            dt.Columns.Add("Mobile", typeof(string));
            dt.Columns.Add("Fax", typeof(string));

            foreach(var item in result)
            {
                dt.Rows.Add(item.Name, item.Type, item.Category, item.Address, item.Telephone, item.Mobile, item.Telephone);
            }

            var grid = new GridView();
            grid.DataSource = dt;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Customer.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "UTF-8";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
}