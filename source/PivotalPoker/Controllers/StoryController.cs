﻿using System.Linq;
using System.Web.Mvc;
using PivotalPoker.Models;

namespace PivotalPoker.Controllers
{
    public class StoryController : Controller
    {
        public StoryController(IPivotal pivotal, IGameRepository games, IGameStarter gameStarter)
        {
            Pivotal = pivotal;
            Games = games;
            GameStarter = gameStarter;
        }

        public IGameRepository Games { get; private set; }
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

            return Status(id);
        }

        public ActionResult Status(int id)
        {
            var game = Games.Get(id);
            var votes = from card in game.GetCards()
                        select new { name = card.Player.Name, vote = game.IsComplete ? card.Value.ToString() : "-" };
            var gameState = new { completed = game.IsComplete, votes };

            return Json(gameState, JsonRequestBehavior.AllowGet);
        }
    }
}