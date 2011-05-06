using NUnit.Framework;
using PivotalPoker.Models;

namespace PivotalPoker.Tests
{
    [TestFixture]
    class PivotalTest
    {
        [Test]
        [Ignore]
        public void CanConnectToPivotal()
        {
            var config = new Config();
            var p = new Pivotal(config.Get<string>("PivotalUserAPIKey"));
            var story = p.GetUnestimatedStory();
            Assert.That(story, Is.Not.Null);
        }
    }
}
