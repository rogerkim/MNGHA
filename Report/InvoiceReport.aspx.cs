using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Homework.Report
{
    public partial class InvoiceReport1 : System.Web.UI.Page
    {
        private Homework.Models.HWEntities db = new Homework.Models.HWEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            ReportDocument rptDoc = new ReportDocument();
            rptDoc.Load(Server.MapPath("InvoiceReport.rpt"));
            rptDoc.SetDataSource(GetData());
            CrystalReportViewer1.ReportSource = rptDoc;
        }

        private InvoiceDataSet GetData()
        {
            int id = int.Parse(Request["param"]); 
            var result = new InvoiceDataSet();
            var invoice = db.invoice.Find(id);

            // Header
            string CustomerID = invoice.customer.customerid.ToString("D5");
            string CustomerName = invoice.customer.name;
            string CustomerAddress = invoice.customer.address;
            string InvoiceID = invoice.invoiceid.ToString("D5");
            string AddDate = String.Format("{0:yyyy/MM/dd HH:mm:ss}", invoice.adddate);
            string Currency = invoice.currency1.description;
            result.HeaderDT.AddHeaderDTRow(CustomerID, CustomerName, CustomerAddress, InvoiceID, AddDate, Currency);

            // Detail
            int i = 0;
            decimal subTotal = 0;
            foreach(var detail in invoice.invoicedetail)
            {
                string lineNo = (++i).ToString("D2");
                string itemCode = detail.itemcode;
                string description = detail.item.description;
                string quantity = detail.quantity.ToString(); 
                string price = string.Format("{0:0.00}", detail.price);
                string total = string.Format("{0:0.00}", (detail.quantity * detail.price));
                subTotal += (decimal) (detail.quantity * detail.price);
                result.DetailDT.AddDetailDTRow(lineNo, description, quantity, price, total, itemCode);
            }

            // Footer
            string SubTotal = string.Format("{0:0.00}", subTotal);
            string Discount = "10";
            string GrandTotal = string.Format("{0:0.00}", subTotal * (decimal) 0.90);
            string TotalWord = DecimalToWords(decimal.Parse(GrandTotal));
            result.Footer.AddFooterRow(SubTotal, Discount, GrandTotal, TotalWord);
            return result;
        }

        private string DecimalToWords(decimal number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + DecimalToWords(Math.Abs(number));

            string words = "";

            int intPortion = (int)number;
            decimal fraction = (number - intPortion) * 100;
            int decPortion = (int)fraction;

            words = NumberToWords(intPortion);
            if (decPortion > 0)
            {
                words += " and ";
                words += NumberToWords(decPortion);
            }
            return words;
        }

        private string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }
}