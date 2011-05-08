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

        /// <summary>
        /// All registered players have played.
        /// </summary>
        public bool IsComplete
        {
            get
            {
                return Players.Count > 1 && Players.All(p => _cards.ContainsKey(p));
            }
        }

        public bool HasConcensus
        {
            get { return GetCards().Select(c => c.Points).Distinct().Count() == 1; }
        }

        private int GetPoints()
        {
            return GetCards().Select(c => c.Points).Distinct().Single();
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void Play(Card card)
        {
            _cards[card.Player] = card;
            CheckForConsensus();
        }

        private void CheckForConsensus()
        {
            if (IsComplete && HasConcensus)
                InvokeConsensus();
        }

        public IEnumerable<Card> GetCards()
        {
            return _cards.Values;
        }

        /// <summary>
        /// Gets passed the points
        /// </summary>
        public event Action<int> Consensus;

        public void InvokeConsensus()
        {
            var handler = Consensus;
            if (handler != null) handler(GetPoints());
        }
    }
}