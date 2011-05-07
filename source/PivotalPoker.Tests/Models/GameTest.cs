using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PivotalPoker.Models;

namespace PivotalPoker.Tests
{
    [TestFixture]
    public class GameTest
    {
        [Test]
        public void CanAddUserToGame()
        {
            var game = new Game();
            game.AddPlayer(new Player {Name = "Rumples"});
            Assert.That(game.Players, Has.Count.EqualTo(1));
        }

        [Test]
        public void PlayerCanPlayACard()
        {
            var game = new Game();
            var player = new Player {Name = "Rumples"};
            game.AddPlayer(player);

            var card = new Card {Player = player, Value = 0};
            game.Play(card);

            var cards = game.GetCards();
            var result = cards.First();
            Assert.That(result.Player.Name, Is.EqualTo(player.Name));
            Assert.That(result.Value, Is.EqualTo(card.Value));
        }

        [Test]
        public void GameIsIncompleteWhenConstructed()
        {
            var game = new Game();
            Assert.That(game.IsComplete, Is.False);
        }

        [Test]
        public void GameIsCompleteWhenAllPlayersHavePlayed()
        {
            var game = new Game();
            var player = new Player {Name = "Rumples"};
            game.AddPlayer(player);
            var card = new Card {Player = player, Value = 0};
            game.Play(card);

            Assert.That(game.IsComplete);
        }

        [Test]
        public void GameIsIncompleteWhenNotAllPlayersHavePlayed()
        {
            var game = new Game();
            var rumples = new Player { Name = "Rumples" };
            game.AddPlayer(rumples);
            var card = new Card { Player = rumples, Value = 0 };
            game.Play(card);

            game.AddPlayer(new Player {Name = "HappyCat"});

            Assert.That(game.IsComplete, Is.False);
        }
    }
}
