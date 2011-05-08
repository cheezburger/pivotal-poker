using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using PivotalPoker.Controllers;
using PivotalPoker.Models;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalPoker.Tests.Controllers
{
    [TestFixture]
    public class ProjectsControllerTest
    {
        [Test]
        public void ReturnsProjects()
        {
            const int projectId = 0;
            const string projectName = "Rumples";
            var pivotalMock = new Mock<IPivotal>();
            pivotalMock.Setup(p => p.GetProjects()).Returns(new[] { new PivotalProject { Id = projectId, Name = projectName } });

            var c = new ProjectsController(pivotalMock.Object);
            var result = (ViewResult)c.Index(null);
            var model = (IEnumerable<PivotalProject>)result.ViewData.Model;

            var project = model.First();
            Assert.That(project.Name, Is.EqualTo(projectName));
            Assert.That(project.Id, Is.EqualTo(0));
        }

        [Test]
        public void RedirectsToFirstUnestimatedStory()
        {
            const int projectId = 123, storyId = 456;
            var pivotalMock = new Mock<IPivotal>();
            pivotalMock.Setup(p => p.GetUnestimatedStory(projectId)).Returns(new PivotalStory { Id = storyId });

            var c = new ProjectsController(pivotalMock.Object);
            var result = (RedirectToRouteResult)c.Index(projectId);
         
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Story"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Detail"));
            Assert.That(result.RouteValues["projectId"], Is.EqualTo(projectId));
            Assert.That(result.RouteValues["storyId"], Is.EqualTo(storyId));
        }
    }
}