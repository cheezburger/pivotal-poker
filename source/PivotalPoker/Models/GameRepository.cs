using System.Collections.Concurrent;

namespace PivotalPoker.Models
{
    public class GameRepository : IGameRepository
    {
        private readonly IPivotal _pivotal;
        private readonly ConcurrentDictionary<int, Game> Games = new ConcurrentDictionary<int, Game>();

        public GameRepository(IPivotal pivotal)
        {
            _pivotal = pivotal;
        }

        public Game Get(int projectId, int storyId)
        {
            return Games.GetOrAdd(storyId, _ =>
            {
                var game = new Game();
                game.Consensus += score => _pivotal.EstimateStory(projectId, storyId, score);
                return game;
            });
        }
    }
}