using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatsApp.Controllers
{
    public class HtmlController : Controller
    {
        // GET: Html
        public ActionResult CatsPage()
        {
            var result = new FilePathResult("~/Views/Html/CatsPage.html", "text/html");
            return result;
        }
    }
}