using System;
using System.Linq;
using System.Web.Mvc;
using PivotalPoker.Models;

namespace PivotalPoker.Controllers
{
    public class StoryController : Controller
    {
        public StoryController(IPivotal pivotal, GameRepository games, IGameStarter gameStarter)
        {
            Pivotal = pivotal;
            Games = games;
            GameStarter = gameStarter;
        }

        public GameRepository Games { get; private set; }
        public IGameStarter GameStarter { get; private set; }

        public IPivotal Pivotal { get; private set; }

        public ActionResult Detail(int id)
        {
            return View(Pivotal.GetStory(id));
        }

        [HttpPost]
        public ActionResult Vote(int id, int score)
        {
            var game = Games.Get(id);
            if (!game.Players.Any(p => p.Name == GameStarter.Name))
                game.AddPlayer(new Player { Name = GameStarter.Name });

            var player = game.Players.Single(p => p.Name == GameStarter.Name);
            game.Play(new Card { Player = player, Value = score });

            return Json("");
        }

        public ActionResult Votes(int id)
        {
            var game = Games.Get(id);
            var votes = from card in game.GetCards()
                        select new { name = card.Player.Name, vote = card.Value };

            return Json(votes, JsonRequestBehavior.AllowGet);
        }
    }
}