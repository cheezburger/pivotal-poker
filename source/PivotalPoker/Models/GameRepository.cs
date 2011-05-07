using System;
using System.Collections.Concurrent;

namespace PivotalPoker.Models
{
    public class GameRepository
    {
        private static readonly ConcurrentDictionary<int, Game> Games = new ConcurrentDictionary<int, Game>();

        public Game Get(int? storyId)
        {
            if (storyId == null)
                return null;

            return Games.GetOrAdd(storyId.Value, _ => new Game());
        }
    }
}