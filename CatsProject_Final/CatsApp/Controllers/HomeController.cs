using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatsApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        //public ActionResult CatsPage()
        //{
        //    var result = new FilePathResult("~/Views/Home/CatsPage.html", "text/html");
        //    return result;
        //}

        //public ActionResult Index()
        //{
        //    return RedirectPermanent("~/Views/Html/default.html");
        //}
    }
}
