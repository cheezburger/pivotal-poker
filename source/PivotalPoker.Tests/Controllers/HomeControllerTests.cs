using System;
using System.Web.Mvc;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PivotalPoker.Controllers;
using PivotalPoker.Models;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalPoker.Tests
{
    [TestFixture]
    internal class HomeControllerTests
    {
        [Test]
        public void IfTheUserHasNoNameShowNameInput()
        {
            var gameStarter = new Mock<IGameStarter>();
            var pivotal = new Mock<IPivotal>();
            var games = new Mock<GameRepository>();
            gameStarter.Setup(gs => gs.Name).Returns("");

            var controller = new HomeController(gameStarter.Object, pivotal.Object, games.Object);
            var result = controller.Index();

            var viewResult = result as ViewResult;
            viewResult.Should().NotBeNull();
        }

        [Test]
        public void IfTheUserHasSetTheirNameRedirectToAStory()
        {
            var gameStarter = new Mock<IGameStarter>();
            var pivotal = new Mock<IPivotal>();
            var games = new Mock<GameRepository>();
            pivotal.Setup(p => p.GetUnestimatedStory()).Returns(new PivotalStory { Id = 1 });
            gameStarter.Setup(gs => gs.Name).Returns("Foo");

            var controller = new HomeController(gameStarter.Object, pivotal.Object, games.Object);
            var result = controller.Index();

            var redirectResult = result as RedirectToRouteResult;
            redirectResult.Should().NotBeNull();
        }

        [Test]
        public void SettingTheNameRedirectsToAStory()
        {
            var gameStarter = new Mock<IGameStarter>();
            var pivotal = new Mock<IPivotal>();
            var games = new Mock<GameRepository>();
            pivotal.Setup(p => p.GetUnestimatedStory()).Returns(new PivotalStory { Id = 1 });

            var controller = new HomeController(gameStarter.Object, pivotal.Object, games.Object);
            var result = controller.Index("Foo");

            gameStarter.VerifySet(gs => gs.Name = "Foo");
            var redirectResult = result as RedirectToRouteResult;
            redirectResult.Should().NotBeNull(); 
        }
    }
}