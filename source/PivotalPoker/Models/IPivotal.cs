using System;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalPoker.Models
{
    public interface IPivotal
    {
        PivotalStory GetUnestimatedStory();
    }
}