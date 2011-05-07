using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PivotalPoker.Models
{
    public class Card
    {
        public Player Player { get; set; }
        public int Value { get; set; }
    }
}