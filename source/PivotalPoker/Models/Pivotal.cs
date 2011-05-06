using System;
using System.Linq;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalPoker.Models
{
    public class Pivotal : IPivotal
    {
        private readonly string _key;
        private readonly PivotalUser _user;

        public Pivotal(string key)
        {
            _key = key;
            _user = new PivotalUser(key);
        }

        public PivotalStory GetUnestimatedStory()
        {
            return PivotalStory.FetchStories(_user, 287145).First(ps => ps.Estimate == -1);
        }
    }
}