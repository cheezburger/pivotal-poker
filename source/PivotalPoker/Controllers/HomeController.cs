using System;
using System.Web.Mvc;
using PivotalPoker.Models;

namespace PivotalPoker.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IGameStarter gameStarter, IPivotal pivotal)
        {
            GameStarter = gameStarter;
            Pivotal = pivotal;
        }

        public IGameStarter GameStarter { get; private set; }

        public IPivotal Pivotal { get; private set; }

        public ActionResult Index()
        {
            if (!String.IsNullOrEmpty(GameStarter.Name))
            {
                var story = Pivotal.GetUnestimatedStory();
                return RedirectToAction("Detail", "Story", new { id = story.Id });
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(string name)
        {
            GameStarter.Name = name;

            var story = Pivotal.GetUnestimatedStory();
            return RedirectToAction("Detail", "story", new { id = story.Id });
        }
    }
}