using Moq;
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
            var gameRepository = new GameRepository(null);
            var game = gameRepository.Get(0);
            Assert.That(game, Is.Not.Null);
        }

        [Test]
        public void CanGetGameInProgress()
        {
            var gameRepository = new GameRepository(null);
            const int storyId = 0;
            var game = gameRepository.Get(storyId);

            var player = new Player {Name = "Rumples"};
            game.AddPlayer(player);

            game = gameRepository.Get(storyId);
            Assert.That(game.Players, Has.Count.EqualTo(1));
        }

        [Test]
        public void UpdatesPivotalWhenGameReachesConsensus()
        {
            const int storyId = 0, points = 0;
            var pivotalMock = new Mock<IPivotal>();

            var gameRepository = new GameRepository(pivotalMock.Object);
            var game = gameRepository.Get(storyId);
            OM.GameM.Play(game, "Rumples", 0);
            OM.GameM.Play(game, "HappyCat", 0);

            pivotalMock.Verify(p => p.EstimateStory(storyId, points));
        }
    }
}
