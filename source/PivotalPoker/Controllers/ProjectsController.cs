using System;
using System.Web.Mvc;
using PivotalPoker.Models;

namespace PivotalPoker.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IPivotal _pivotal;

        public ProjectsController(IPivotal pivotal)
        {
            _pivotal = pivotal;
        }

        public ActionResult Index()
        {
            var projects = _pivotal.GetProjects();
            return View(projects);
        }
    }
}