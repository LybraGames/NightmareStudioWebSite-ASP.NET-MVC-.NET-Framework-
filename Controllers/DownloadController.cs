using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NightmareStudioWebSite_ASP.NET_MVC_.NET_Framework_.Controllers
{
    public class DownloadController : Controller
    {
        public ActionResult Download()
        {
            return View("Download");
        }
    }
}