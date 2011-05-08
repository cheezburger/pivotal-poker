using System.Web.Mvc;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PivotalPoker.Controllers;
using PivotalPoker.Models;

namespace PivotalPoker.Tests.Controllers
{
    [TestFixture]
    internal class HomeControllerTests
    {
        [Test]
        public void IfTheUserHasNoNameShowNameInput()
        {
            var gameStarter = new Mock<IGameStarter>();
            gameStarter.Setup(gs => gs.Name).Returns("");

            var controller = new HomeController(gameStarter.Object);
            var result = controller.Index();

            var viewResult = result as ViewResult;
            viewResult.Should().NotBeNull();
        }

        [Test]
        public void SettingTheNameRedirectsToProjects()
        {
            var gameStarter = new Mock<IGameStarter>();
            gameStarter.SetupProperty(g => g.Name);
            var controller = new HomeController(gameStarter.Object);
            var result = (RedirectToRouteResult)controller.Index("Foo");

            gameStarter.VerifySet(gs => gs.Name = "Foo");
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Projects"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void IndexRedirectsToProjectsIfNameIsSet()
        {
            var gameStarter = new Mock<IGameStarter>();
            gameStarter.Setup(gs => gs.Name).Returns("Foo");

            var controller = new HomeController(gameStarter.Object);
            var result = (RedirectToRouteResult)controller.Index();

            Assert.That(result.RouteValues["controller"], Is.EqualTo("Projects"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }
    }
}