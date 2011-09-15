using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using PivotalPoker.Controllers;
using PivotalPoker.Models;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalPoker.Tests.Controllers
{
    [TestFixture]
    public class StoryControllerTest
    {
        [Test]
        public void VoteMayCauseGameConsensus()
        {
            const int points = 0, projectId = 123, storyId = 456;

            var gameRepositoryMock = new Mock<IGameRepository>();
            var gameStarterMock = new Mock<IGameStarter>();

            gameStarterMock.Setup(o => o.Name).Returns("Rumples");

            var returnedScore = -1;
            var game = new Game();
            game.Consensus += score => returnedScore = score;

            var james = new Player { Name = "James" };
            game.AddPlayer(james);
            game.Play(new Card { Player = james, Points = points });

            var rumples = new Player { Name = "Rumples" };
            game.AddPlayer(rumples);

            gameRepositoryMock.Setup(g => g.Get(projectId, storyId)).Returns(game);

            var c = new StoryController(null, gameRepositoryMock.Object, gameStarterMock.Object);
            c.Vote(projectId, storyId, points);

            Assert.That(returnedScore, Is.EqualTo(points));
        }

        [Test]
        public void CanResetGame()
        {
            const int projectId = 123, storyId = 456;

            var gameMock = new Mock<Game>();
            var gameRepositoryMock = new Mock<IGameRepository>();
            gameRepositoryMock.Setup(g => g.Get(projectId, storyId)).Returns(gameMock.Object);
            
            var c = new StoryController(null, gameRepositoryMock.Object, null);
            c.Reset(projectId, storyId);

            gameMock.Verify(g => g.Reset());
        }
    }
}
