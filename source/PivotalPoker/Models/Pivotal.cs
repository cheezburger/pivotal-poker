using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalPoker.Models
{
    public class Pivotal : IPivotal
    {
        private readonly PivotalUser _user;

        public Pivotal()
        {
            var key = ConfigurationManager.AppSettings["PivotalUserAPIKey"];
            _user = new PivotalUser(key);
        }

        public PivotalStory GetUnestimatedStory()
        {
            return PivotalStory.FetchStories(_user, 287145).First(ps => ps.Estimate == -1);
        }
    }
}