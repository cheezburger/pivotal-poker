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

        [Test, Ignore]
        public void CanAssignPointsToAStory()
        {
            const int storyId = 13115015;
            var p = new Pivotal();
            p.EstimateStory(storyId, 1);

            var story = p.GetStory(storyId);

            Assert.That(story.Estimate, Is.EqualTo(1));
        }
    }
}
