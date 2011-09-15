using NUnit.Framework;
using PivotalPoker.Models;

namespace PivotalPoker.Tests.Models
{
    [TestFixture]
    class PivotalTest
    {
        [Test, Explicit]
        public void CanConnectToPivotal()
        {
            var config = new Config();
            var p = new Pivotal(config.Get<string>("PivotalUserAPIKey"));
            var story = p.GetUnestimatedStory(0);
            Assert.That(story, Is.Not.Null);
        }

        [Test, Explicit]
        public void CanAssignPointsToAStory()
        {
            const int projectId = 0, storyId = 13115015;
            var config = new Config();
            var p = new Pivotal(config.Get<string>("PivotalUserAPIKey"));
            p.EstimateStory(projectId, storyId, 1);

            var story = p.GetStory(0, storyId);

            Assert.That(story.Estimate, Is.EqualTo(1));
        }
    }
}
