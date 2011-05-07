using System;
using System.Collections.Generic;

namespace PivotalPoker.Models
{
    public class Game
    {
        public Game()
        {
            Players = new List<Player>();
        }

        public string Id { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}