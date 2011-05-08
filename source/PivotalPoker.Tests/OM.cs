using PivotalPoker.Models;

namespace PivotalPoker.Tests
{
    class OM
    {
        public class GameM
        {
            public static Game Play(Game game, string playerName, int score)
            {
                if (game == null)
                    game = new Game();
                var rumples = new Player { Name = playerName };
                game.AddPlayer(rumples);
                var card = new Card { Player = rumples, Value = score };
                game.Play(card);
                return game;
            }
        }
    }
}
