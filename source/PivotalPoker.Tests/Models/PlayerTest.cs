using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PivotalPoker.Models;

namespace PivotalPoker.Tests
{
    [TestFixture]
    public class PlayerTest
    {
        [Test]
        public void HashesOutToTheNamesHash()
        {
            const string name = "Rumples";
            var player = new Player {Name = name};
            Assert.That(player.GetHashCode(), Is.EqualTo(name.GetHashCode()));
        }
    }
}
