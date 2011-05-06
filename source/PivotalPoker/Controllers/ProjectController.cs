using System;
using PivotalPoker.Models;

namespace PivotalPoker.Controllers
{
    public class ProjectController
    {
        public ProjectController(IPivotal pivotal)
        {
            Pivotal = pivotal;
        }

        public IPivotal Pivotal { get; private set; }
    }
}