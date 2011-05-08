using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using PivotalPoker.Controllers;
using PivotalPoker.Models;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalPoker.Tests.Controllers
{
    [TestFixture]
    public class StoryControllerTests
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
        public void ReturnsStoryDetails()
        {
            const int projectId = 123, storyId = 456;
            var pivotalMock = new Mock<IPivotal>();
            pivotalMock.Setup(p => p.GetStory(projectId, storyId)).Returns(new PivotalStory { Id = storyId, ProjectId = projectId });

            var game = new Game();
            var gameRepositoryMock = new Mock<IGameRepository>();
            gameRepositoryMock.Setup(g => g.Get(projectId, storyId)).Returns(game);
            var gameStarterMock = new Mock<IGameStarter>();

            var c = new StoryController(pivotalMock.Object, gameRepositoryMock.Object, gameStarterMock.Object);
            var result = (ViewResult)c.Detail(projectId, storyId);
            var model = (PivotalStory)result.Model;

            Assert.That(model.ProjectId, Is.EqualTo(projectId));
            Assert.That(model.Id, Is.EqualTo(storyId));
        }

        [Test]
        public void AddsPlayerToGameOnViewOfStory()
        {
            const int projectId = 123, storyId = 456;
            var pivotalMock = new Mock<IPivotal>();
            pivotalMock.Setup(p => p.GetStory(projectId, storyId)).Returns(new PivotalStory { Id = storyId, ProjectId = projectId });

            var game = new Game();
            var gameRepositoryMock = new Mock<IGameRepository>();
            gameRepositoryMock.Setup(g => g.Get(projectId, storyId)).Returns(game);

            const string playerName = "Rumples";
            var gameStarterMock = new Mock<IGameStarter>();
            gameStarterMock.Setup(g => g.Name).Returns(playerName);

            var c = new StoryController(pivotalMock.Object, gameRepositoryMock.Object, gameStarterMock.Object);
            c.Detail(projectId, storyId);

            Assert.That(game.Players, Has.Some.Property("Name").EqualTo(playerName));
        }

        [Test]
        public void WontAddExistingPlayerToGame()
        {
            const int projectId = 123, storyId = 456;
            var pivotalMock = new Mock<IPivotal>();
            pivotalMock.Setup(p => p.GetStory(projectId, storyId)).Returns(new PivotalStory { Id = storyId, ProjectId = projectId });

            var game = new Game();
            var gameRepositoryMock = new Mock<IGameRepository>();
            gameRepositoryMock.Setup(g => g.Get(projectId, storyId)).Returns(game);

            const string playerName = "Rumples";
            var gameStarterMock = new Mock<IGameStarter>();
            gameStarterMock.Setup(g => g.Name).Returns(playerName);

            var c = new StoryController(pivotalMock.Object, gameRepositoryMock.Object, gameStarterMock.Object);
            c.Detail(projectId, storyId);
            c.Detail(projectId, storyId);

            Assert.That(game.Players, Has.Count.EqualTo(1));
        }
    }
}
