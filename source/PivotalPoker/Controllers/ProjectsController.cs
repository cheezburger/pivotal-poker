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

        private ActionResult Index()
        {
            var projects = _pivotal.GetProjects();
            return View(projects);
        }

        public ActionResult Index(int? projectId)
        {
            if (projectId == null)
                return Index();

            var story = _pivotal.GetUnestimatedStory(projectId.Value);
            return RedirectToAction("Detail", "Story", new { storyId = story.Id, projectId = projectId.Value });
        }
    }
}