using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homework.Models;
using Homework.VM;

namespace Homework.Controllers
{
    public class Section1Controller : Controller
    {
        private HWEntities db = new HWEntities();

        // GET: section1
        public ActionResult Index()
        {
            var result = db.invoice.Include(m => m.customer).ToList();
            var model = new List<InvoiceVM>();
            foreach (invoice invoice in result)
            {
                InvoiceVM vm = new InvoiceVM();
                vm.CustomerName = invoice.customer.name;
                vm.InvoiceID = invoice.invoiceid.ToString("D5");
                vm.AddDate = String.Format("{0:yyyy/MM/dd HH:mm:ss}", invoice.adddate);
                model.Add(vm);
            }
            return View(model);
        }

        // GET: section1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var invoice = db.invoice.Include(m => m.customer).Include(m => m.invoicedetail).Where(m => m.invoiceid == id).FirstOrDefault();
            if (invoice == null)
            {
                return HttpNotFound();
            }

            // View Model
            var model = new InvoiceVM();

            // Header
            model.CustomerID = invoice.customer.customerid.ToString("D5");
            model.CustomerName = invoice.customer.name;
            model.CustomerAddress = invoice.customer.address;
            model.InvoiceID = invoice.invoiceid.ToString("D5");
            model.AddDate = String.Format("{0:yyyy/MM/dd HH:mm:ss}", invoice.adddate);
            model.Currency = invoice.currency1.description;
            ViewBag.IntInvoiceID = invoice.invoiceid;

            // Detail
            var invoiceDetailList = invoice.invoicedetail;
            foreach(var invoiceDetail in invoiceDetailList)
            {
                var detailVM = new InvoiceDetailVM();
                detailVM.Description = invoiceDetail.item.description;
                detailVM.ItemCode = invoiceDetail.itemcode;
                detailVM.Price = (decimal) invoiceDetail.price;
                detailVM.Qty = (int) invoiceDetail.quantity;
                model.Detail.Add(detailVM);
            }

            return View(model);
        }

        // GET: section1/Create
        public ActionResult Create()
        {
            ViewBag.CustomerList = new SelectList(db.customer, "customerid", "name");
            ViewBag.CurrencyList = new SelectList(db.currency, "currencycode", "description");
            ViewBag.ItemList = new SelectList(db.item, "itemcode", "description");
            return View();
        }

        [HttpPost]
        public ActionResult Create(InvoiceVM model)
        {
            // Header
            var invoice = new invoice();
            invoice.customerid = Int32.Parse(model.CustomerID);
            invoice.currencycode = model.Currency;
            invoice.adddate = DateTime.Now;
            invoice.editdate = DateTime.Now;

            // Detail
            List<invoicedetail> detailList = new List<invoicedetail>();
            for (int i=0; i < model.Detail.Count; i++)
            {
                var source = model.Detail[i];
                var detail = new invoicedetail();
                detail.itemcode = source.ItemCode;
                detail.price = source.Price;
                detail.quantity = source.Qty;
                detail.adddate = DateTime.Now;
                detail.editdate = DateTime.Now;
                detailList.Add(detail);
            }

            // Assign detail to header
            invoice.invoicedetail = detailList;
            db.invoice.Add(invoice);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: section1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var invoice = db.invoice.Include(m => m.customer).Include(m => m.invoicedetail).Where(m => m.invoiceid == id).FirstOrDefault();
            if (invoice == null)
            {
                return HttpNotFound();
            }
            
            var model = new InvoiceVM();

            // Header
            model.CustomerID = invoice.customerid.ToString();
            model.Currency = invoice.currencycode;

            // Detail
            foreach(var detail in invoice.invoicedetail)
            {
                var detailVM = new InvoiceDetailVM();
                detailVM.ItemCode = detail.itemcode;
                detailVM.Description = detail.item.description;
                detailVM.Qty = (int) detail.quantity;
                detailVM.Price = (decimal) detail.price;
                model.Detail.Add(detailVM);
            }

            ViewBag.CustomerList = new SelectList(db.customer, "customerid", "name");
            ViewBag.CurrencyList = new SelectList(db.currency, "currencycode", "description");
            ViewBag.ItemList = new SelectList(db.item, "itemcode", "description");
            ViewBag.InvoiceID = id;
            return View(model);
        }

        // POST: section1/Edit/5
        [HttpPost]
        public ActionResult Edit(InvoiceVM model)
        {
            int invoiceID = int.Parse(model.InvoiceID);

            // Remove Detail
            db.invoicedetail.RemoveRange(db.invoicedetail.Where(x => x.invoiceid == invoiceID));

            // Insert Detail
            List<invoicedetail> detailList = new List<invoicedetail>();
            for (int i = 0; i < model.Detail.Count; i++)
            {
                var source = model.Detail[i];
                var detail = new invoicedetail();
                detail.itemcode = source.ItemCode;
                detail.price = source.Price;
                detail.quantity = source.Qty;
                detail.itemcode = source.ItemCode;
                detail.adddate = DateTime.Now;
                detail.editdate = DateTime.Now;
                detailList.Add(detail);
            }
            db.invoice.Find(invoiceID).invoicedetail = detailList;

            // Update Header
            db.invoice.Find(invoiceID).customerid = int.Parse(model.CustomerID);
            db.invoice.Find(invoiceID).currencycode = model.Currency;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: section1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            invoice invoice = db.invoice.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }

            // remove invoice detail
            db.invoicedetail.RemoveRange(db.invoicedetail.Where(x => x.invoiceid == id));

            // remove invoice header
            db.invoice.Remove(invoice);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
