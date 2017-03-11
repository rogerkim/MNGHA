using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Section4()
        {
            ViewBag.Message = "Research";

            return View();
        }

        public ActionResult ERD()
        {
            ViewBag.Message = "ERD";

            return View();
        }
    }
}