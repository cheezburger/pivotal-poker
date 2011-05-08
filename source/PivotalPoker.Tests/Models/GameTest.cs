using System.Linq;
using NUnit.Framework;
using PivotalPoker.Models;

namespace PivotalPoker.Tests.Models
{
    [TestFixture]
    public class GameTest
    {
        [Test]
        public void CanAddUserToGame()
        {
            var game = new Game();
            game.AddPlayer(new Player {Name = "Rumples"});
            Assert.That(game.Players, Has.Count.EqualTo(1));
        }

        [Test]
        public void PlayerCanPlayACard()
        {
            var game = new Game();
            var player = new Player {Name = "Rumples"};
            game.AddPlayer(player);

            var card = new Card {Player = player, Points = 0};
            game.Play(card);

            var cards = game.GetCards();
            var result = cards.First();
            Assert.That(result.Player.Name, Is.EqualTo(player.Name));
            Assert.That(result.Points, Is.EqualTo(card.Points));
        }

        [Test]
        public void GameIsIncompleteWhenConstructed()
        {
            var game = new Game();
            Assert.That(game.IsComplete, Is.False);
        }

        [Test]
        public void GameIsCompleteWhenAllPlayersHavePlayed()
        {
            var game = OM.GameM.Play(null, "Rumples", 0);
            OM.GameM.Play(game, "HappyCat", 1);

            Assert.That(game.IsComplete);
        }

        [Test]
        public void GameIsIncompleteWhenNotAllPlayersHavePlayed()
        {
            var game = OM.GameM.Play(null, "Rumples", 0);
            game.AddPlayer(new Player {Name = "HappyCat"});

            Assert.That(game.IsComplete, Is.False);
        }

        [Test]
        public void GameIsIncompleteUnlessAtLeastTwoPlayersHavePlayed()
        {
            var game = Play(null, "Rumples", 0);

            Assert.That(game.IsComplete, Is.False);
        }

        [Test]
        public void GameIndicatesIfAllPlayersAgree()
        {
            var game = Play(null, "Rumples", 1);
            Play(game, "HappyCat", 1);
            
            Assert.That(game.HasConcensus);
        }

        [Test]
        public void GameIndicatesIfAllPlayersDontAgree()
        {
            var game = Play(null, "Rumples", 1);
            Play(game, "HappyCat", 0);

            Assert.That(game.HasConcensus, Is.False);
        }

        private static Game Play(Game game, string playerName, int score)
        {
            return OM.GameM.Play(game, playerName, score);
        }

        [Test]
        public void GameAnnouncesConcensus()
        {
            var game = new Game();
            var returnedScore = -1;
            game.Consensus +=  score =>
            {
                returnedScore = score;
            };

            Play(game, "Rumples", 1);
            Play(game, "HappyCat", 1);

            Assert.That(returnedScore, Is.EqualTo(1));
        }

        [Test]
        public void CanResetGame()
        {
            var game = Play(null, "Rumples", 0);
            Play(game, "HappyCat", 1);

            game.Reset();

            Assert.That(game.GetCards(), Is.Empty);
        }
    }
}
