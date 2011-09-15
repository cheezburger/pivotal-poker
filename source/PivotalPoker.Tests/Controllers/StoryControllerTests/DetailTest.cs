using System;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using PivotalPoker.Controllers;
using PivotalPoker.Models;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalPoker.Tests.Controllers.StoryControllerTests
{
    [TestFixture]
    public class DetailTest
    {
        class Helper
        {
            public const int ProjectId = 123;
            public const int StoryId = 456;

            private readonly Mock<IPivotal> _pivotalMock;
            private readonly Mock<IGameRepository> _gameRepositoryMock;
            private readonly Mock<IGameStarter> _gameStarterMock;

            public Game Game { get; private set; }

            public Helper()
            {
                _pivotalMock = new Mock<IPivotal>();
                _gameRepositoryMock = new Mock<IGameRepository>();
                _gameStarterMock = new Mock<IGameStarter>();

                _pivotalMock.Setup(p => p.GetStory(ProjectId, StoryId)).Returns(new PivotalStory { Id = StoryId, ProjectId = ProjectId });
                _pivotalMock.Setup(p => p.GetProject(ProjectId)).Returns(new PivotalProject { PointScale = "0,1,2,3" });

                Game = new Game();
                _gameRepositoryMock.Setup(g => g.Get(ProjectId, StoryId)).Returns(Game);
            }

            public StoryController Create()
            {
                return new StoryController(_pivotalMock.Object, _gameRepositoryMock.Object, _gameStarterMock.Object);
            }

            public void GameStarterName(string playerName)
            {
                _gameStarterMock.Setup(g => g.Name).Returns(playerName);
            }
        }

        [Test]
        public void CanGetStoryDetailsAndPointScale()
        {
            var helper = new Helper();

            var c = helper.Create();

            var result = (ViewResult)c.Detail(Helper.ProjectId, Helper.StoryId);
            var model = (StoryController.DetailModel)result.Model;
            var story = model.Story;
            var pointScaleOptions = model.PointScaleOptions;

            Assert.That(story.ProjectId, Is.EqualTo(Helper.ProjectId));
            Assert.That(story.Id, Is.EqualTo(Helper.StoryId));
            Assert.That(pointScaleOptions, Is.EquivalentTo(new[] { 0, 1, 2, 3 }));
        }

        [Test]
        public void AddsPlayerToGameOnViewOfStory()
        {
            const string playerName = "Rumples";

            var helper = new Helper();
            helper.GameStarterName(playerName);

            var c = helper.Create();
            c.Detail(Helper.ProjectId, Helper.StoryId);

            Assert.That(helper.Game.Players, Has.Some.Property("Name").EqualTo(playerName));
        }

        [Test]
        public void WontAddExistingPlayerToGame()
        {
            const string playerName = "Rumples";

            var helper = new Helper();
            helper.GameStarterName(playerName);

            var c = helper.Create();
            c.Detail(Helper.ProjectId, Helper.StoryId);
            c.Detail(Helper.ProjectId, Helper.StoryId);

            Assert.That(helper.Game.Players, Has.Count.EqualTo(1));
        }
    }
}
