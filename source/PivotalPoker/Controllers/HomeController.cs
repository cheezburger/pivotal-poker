using System;
using System.Web.Mvc;
using PivotalPoker.Models;

namespace PivotalPoker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameStarter _gameStarter;
        
        public HomeController(IGameStarter gameStarter)
        {
            _gameStarter = gameStarter;
        }

        public ActionResult Index()
        {
            if (!String.IsNullOrEmpty(_gameStarter.Name)) 
                return RedirectToAction("Index", "Projects");
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(string name)
        {
            _gameStarter.Name = name;
            return Index();
        }
    }
}