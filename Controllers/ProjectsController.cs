using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NightmareStudioWebSite_ASP.NET_MVC_.NET_Framework_.Controllers
{
    public class ProjectsController : Controller
    {
        [HttpPost]
        public ActionResult Projects(string Project)
        {
            switch (Project) 
            {
                case "JustJump":
                    return View("JustJump");
               
                case "EndlessFire":
                    return View("EndlessFire");
                
                case "TheWalls":
                    return View("TheWalls");

                case "TheBean":
                    return View("TheBean");

                case "LevelDesign":
                    return View("LevelDesign");

                default:
                    return View("Projects");
               
            }
        }
    }
}