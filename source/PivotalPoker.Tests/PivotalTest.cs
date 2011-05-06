using NUnit.Framework;
using PivotalPoker.Models;

namespace PivotalPoker.Tests
{
    [TestFixture]
    class PivotalTest
    {
        [Test]
        public void CanConnectToPivotal()
        {
            var p = new Pivotal();
            var story = p.GetUnestimatedStory();
            Assert.That(story, Is.Not.Null);
        }
    }
}
