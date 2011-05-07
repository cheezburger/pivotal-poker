using System;
using System.Web.Mvc;
using PivotalPoker.Models;

namespace PivotalPoker.Controllers
{
    public class StoryController : Controller
    {
        public StoryController(IPivotal pivotal)
        {
            Pivotal = pivotal;
        }

        public IPivotal Pivotal { get; private set; }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Next()
        {
            return Json(Pivotal.GetUnestimatedStory(), JsonRequestBehavior.AllowGet);
        }
    }
}