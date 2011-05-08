using System;
using System.Web.Mvc;
using PivotalPoker.Models;

namespace PivotalPoker.Controllers
{
    public class HomeController : Controller
    {
        public IGameStarter GameStarter { get; private set; }
        public IPivotal Pivotal { get; private set; }
        public IGameRepository Games { get; private set; }
        
        public HomeController(IGameStarter gameStarter, IPivotal pivotal, IGameRepository games)
        {
            GameStarter = gameStarter;
            Pivotal = pivotal;
            Games = games;
        }

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
            var game = Games.Get(story.Id.Value);
            if (game != null)
                game.AddPlayer(new Player { Name = name });

            return RedirectToAction("detail", "story", new { id = story.Id });
        }
    }
}