using System.Collections.Generic;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalPoker.Models
{
    public interface IPivotal
    {
        PivotalStory GetUnestimatedStory(int projectId);
        void EstimateStory(int projectId, int storyId, int points);
        PivotalStory GetStory(int projectId, int storyId);
        IEnumerable<PivotalProject> GetProjects();
        PivotalProject GetProject(int projectId);
        void LoadTasks(PivotalStory story);
    }
}