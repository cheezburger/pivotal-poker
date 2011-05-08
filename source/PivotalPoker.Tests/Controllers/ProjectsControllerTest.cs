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
            var result = (ViewResult)c.Index();
            var model = (IEnumerable<PivotalProject>)result.ViewData.Model;

            var project = model.First();
            Assert.That(project.Name, Is.EqualTo(projectName));
            Assert.That(project.Id, Is.EqualTo(0));
        }
    }
}
