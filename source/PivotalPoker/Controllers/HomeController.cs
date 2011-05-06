using System;
using System.Web.Mvc;

namespace PivotalPoker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}