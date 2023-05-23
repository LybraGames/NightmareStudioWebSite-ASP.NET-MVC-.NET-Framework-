using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NightmareStudioWebSite_ASP.NET_MVC_.NET_Framework_.Controllers
{
    public class mediaController : Controller
    {
        [HttpPost]
        public ActionResult Media(string media)
        {
            return View("Media");
        }
    }
}