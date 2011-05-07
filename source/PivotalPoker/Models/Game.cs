using System;
using System.Linq;
using System.Collections.Generic;

namespace PivotalPoker.Models
{
    /// <summary>
    /// Each story has one game
    /// </summary>
    public class Game
    {

        private readonly IDictionary<Player, Card> _cards = new Dictionary<Player, Card>(10);
        public Game()
        {
            Players = new List<Player>();
        }

        public ICollection<Player> Players { get; set; }

        public bool IsComplete
        {
            get
            {
                return Players.Count > 0 && Players.All(p => _cards.ContainsKey(p));
            }
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void Play(Card card)
        {
            _cards[card.Player] = card;
        }

        public IEnumerable<Card> GetCards()
        {
            return _cards.Values;
        }
    }
}