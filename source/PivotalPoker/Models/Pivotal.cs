using System;
using System.Collections.Generic;
using System.Linq;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalPoker.Models
{
    public class Pivotal : IPivotal
    {
        private readonly PivotalUser _user;
        public Pivotal(string key)
        {
            _user = new PivotalUser(key);
        }

        private const int Unestimated = -1;
        public PivotalStory GetUnestimatedStory(int projectId)
        {
            return PivotalStory.FetchStories(_user, projectId).FirstOrDefault(ps => ps.Estimate == Unestimated);
        }

        public void EstimateStory(int projectId, int storyId, int points)
        {
            var story = GetStory(projectId, storyId);
            story.Estimate = points;
            story.UpdateStory(_user);
        }

        // internal
        public PivotalStory GetStory(int projectId, int storyId)
        {
            return PivotalStory.FetchStory(_user, projectId, storyId.ToString());
        }

        public IEnumerable<PivotalProject> GetProjects()
        {
            var projects = PivotalProject.FetchProjects(_user);
            return projects;
        }

        /// <summary>
        /// Does not fetch stories.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public PivotalProject GetProject(int projectId)
        {
            return PivotalProject.FetchProject(_user, projectId, false);
        }

        public void LoadTasks(PivotalStory story)
        {
            story.LoadTasks(_user);
        }

        public void LoadNotes(PivotalStory story)
        {
            story.LoadNotes(_user);
        }
    }
}
