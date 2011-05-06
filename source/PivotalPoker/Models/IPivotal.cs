using System;
using System.Collections.Generic;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalPoker.Models
{
    public interface IPivotal
    {
        PivotalStory GetUnestimatedStory();
    }
}