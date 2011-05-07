using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PivotalPoker.Models
{
    public class GameRepository
    {
        private static readonly ConcurrentDictionary<int, Game> Games = new ConcurrentDictionary<int, Game>();

        public Game Get(int storyId)
        {
            return Games.GetOrAdd(storyId, _ => new Game());
        }
    }
}