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
    }
}