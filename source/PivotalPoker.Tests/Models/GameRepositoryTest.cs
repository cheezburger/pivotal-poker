using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PivotalPoker.Models;

namespace PivotalPoker.Tests.Models
{
    [TestFixture]
    public class GameRepositoryTest
    {
        [Test]
        public void GettingNonExistantGameReturnsNewGame()
        {
            var gameRepository = new GameRepository();
            var game = gameRepository.Get(0);
            Assert.That(game, Is.Not.Null);
        }

        [Test]
        public void CanGetGameInProgress()
        {
            var gameRepository = new GameRepository();
            const int storyId = 0;
            var game = gameRepository.Get(storyId);

            var player = new Player {Name = "Rumples"};
            game.AddPlayer(player);

            game = gameRepository.Get(storyId);
            Assert.That(game.Players, Has.Count.EqualTo(1));
        }
    }
}
