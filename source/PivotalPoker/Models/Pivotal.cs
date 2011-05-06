using System;
using System.Linq;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalPoker.Models
{
    public class Pivotal : IPivotal
    {
        private readonly string _key;
        private readonly PivotalUser _user;
        private const int _projectId = 287145;
        public Pivotal(string key)
        {
            _key = key;
            _user = new PivotalUser(key);
        }

        public PivotalStory GetUnestimatedStory()
        {
            return PivotalStory.FetchStories(_user, 287145).First(ps => ps.Estimate == -1);
        }

        public void EstimateStory(int storyId, int? points)
        {
            var story = GetStory(storyId);
            story.Estimate = points;
            story.UpdateStory(_user);
        }

        // internal
        public PivotalStory GetStory(int storyId)
        {
            return PivotalStory.FetchStory(_user, _projectId, storyId.ToString());
        }
    }
}
