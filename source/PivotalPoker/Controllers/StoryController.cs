using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PivotalPoker.Models;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalPoker.Controllers
{
    public class StoryController : Controller
    {
        private readonly IPivotal _pivotal;
        private readonly IGameRepository _games;
        private readonly IGameStarter _gameStarter;

        public StoryController(IPivotal pivotal, IGameRepository games, IGameStarter gameStarter)
        {
            _pivotal = pivotal;
            _games = games;
            _gameStarter = gameStarter;
        }

        private string CurrentUserName { get { return _gameStarter.Name; } }

        public class DetailModel
        {
            public PivotalStory Story { get; set; }
            public IEnumerable<int> PointScaleOptions { get; set; }
        }

        public ActionResult Detail(int projectId, int storyId)
        {
            if (string.IsNullOrEmpty(CurrentUserName))
                return RedirectToAction("Index", "Home");

            var game = _games.Get(projectId, storyId);
            EnsurePlayerExists(game, CurrentUserName);

            var project = _pivotal.GetProject(projectId);
            var pointScaleOptions = project.PointScale.Split(',').Select(n => int.Parse(n));
            var story = _pivotal.GetStory(projectId, storyId);
            _pivotal.LoadTasks(story);
            
            var model = new DetailModel
            {
                Story = story,
                PointScaleOptions = pointScaleOptions
            };

            return View(model);
        }

        private static void EnsurePlayerExists(Game game, string playerName)
        {
            if (!game.Players.Any(p => p.Name == playerName))
                game.AddPlayer(new Player { Name = playerName });
        }

        [HttpPost]
        public ActionResult Vote(int projectId, int storyId, int score)
        {
            var game = _games.Get(projectId, storyId);
            EnsurePlayerExists(game, CurrentUserName);

            var player = game.Players.Single(p => p.Name == CurrentUserName);
            game.Play(new Card { Player = player, Points = score });

            return Status(projectId, storyId);
        }

        public ActionResult Status(int projectId, int storyId)
        {
            var game = _games.Get(projectId, storyId);
            var votes = from player in game.Players
                        join card in game.GetCards()
                        on player equals card.Player into playerCard
                        from card in playerCard.DefaultIfEmpty()
                        select new { name = player.Name, vote = RenderPoint(game.IsComplete, card) };
            var gameState = new { completed = game.IsComplete, votes };

            return Json(gameState, JsonRequestBehavior.AllowGet);
        }

        private static string RenderPoint(bool isComplete, Card card)
        {
            if (card == null)
                return "?";
            if (!isComplete)
                return "-";

            return card.Points.ToString();
        }

        [HttpPost]
        public ActionResult Reset(int projectId, int storyId)
        {
            _games.Get(projectId, storyId).Reset();
            return Content(string.Empty);
        }
    }
}