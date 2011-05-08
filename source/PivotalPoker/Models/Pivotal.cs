using System;
using System.Collections.Generic;
using System.Linq;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalPoker.Models
{
    public class Pivotal : IPivotal
    {
        private readonly PivotalUser _user;
        private const int ProjectId = 287145;
        public Pivotal(string key)
        {
            _user = new PivotalUser(key);
        }

        private const int Unestimated = -1;
        public PivotalStory GetUnestimatedStory()
        {
            return PivotalStory.FetchStories(_user, ProjectId).FirstOrDefault(ps => ps.Estimate == Unestimated);
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
            return PivotalStory.FetchStory(_user, ProjectId, storyId.ToString());
        }

        public IEnumerable<PivotalProject> GetProjects()
        {
            return PivotalProject.FetchProjects(_user);
        }
    }
}
