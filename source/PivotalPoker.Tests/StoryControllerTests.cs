using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PivotalPoker.Controllers;
using PivotalPoker.Models;

namespace PivotalPoker.Tests
{
    [TestFixture]
    public class StoryControllerTests
    {
        [Test]
        public void CanGetNextStory()
        {
            var pivotal = Mock.Of<IPivotal>();

            var controller = new StoryController(pivotal);
            var story = controller.Next();

            story.Should().NotBeNull();
        }
    }
}