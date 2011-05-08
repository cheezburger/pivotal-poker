using PivotalPoker.Models;

namespace PivotalPoker.Tests
{
    class OM
    {
        public class GameM
        {
            public static Game Play(Game game, string playerName, int points)
            {
                if (game == null)
                    game = new Game();
                var rumples = new Player { Name = playerName };
                game.AddPlayer(rumples);
                var card = new Card { Player = rumples, Points = points };
                game.Play(card);
                return game;
            }
        }
    }
}
