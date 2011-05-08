using System.Collections.Generic;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalPoker.Models
{
    public interface IPivotal
    {
        PivotalStory GetUnestimatedStory();
        void EstimateStory(int storyId, int? points);
        PivotalStory GetStory(int storyId);
        IEnumerable<PivotalProject> GetProjects();
    }
}