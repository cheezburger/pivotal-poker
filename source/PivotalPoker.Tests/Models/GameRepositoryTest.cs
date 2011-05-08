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
            const int projectId = 123, storyId = 456;
            var gameRepository = new GameRepository(null);
            var game = gameRepository.Get(projectId, storyId);
            Assert.That(game, Is.Not.Null);
        }

        [Test]
        public void CanGetGameInProgress()
        {
            var gameRepository = new GameRepository(null);
            const int storyId = 0;
            var game = gameRepository.Get(-1, storyId);

            var player = new Player { Name = "Rumples" };
            game.AddPlayer(player);

            game = gameRepository.Get(-1, storyId);
            Assert.That(game.Players, Has.Count.EqualTo(1));
        }

        [Test]
        public void UpdatesPivotalWhenGameReachesConsensus()
        {
            const int projectId = 123, storyId = 456, points = 0;
            var pivotalMock = new Mock<IPivotal>();

            var gameRepository = new GameRepository(pivotalMock.Object);
            var game = gameRepository.Get(projectId, storyId);
            OM.GameM.Play(game, "Rumples", points);
            OM.GameM.Play(game, "HappyCat", points);

            pivotalMock.Verify(p => p.EstimateStory(projectId, storyId, points));
        }
    }
}
