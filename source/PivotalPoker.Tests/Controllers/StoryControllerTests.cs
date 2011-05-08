using Moq;
using NUnit.Framework;
using PivotalPoker.Controllers;
using PivotalPoker.Models;

namespace PivotalPoker.Tests.Controllers
{
    [TestFixture]
    public class StoryControllerTests
    {
        [Test]
        public void VoteMayCauseGameConsensus()
        {
            const int masterScore = 0;
            const int storyId = 0;

            var gameRepositoryMock = new Mock<IGameRepository>();
            var gameStarterMock = new Mock<IGameStarter>();

            gameStarterMock.Setup(o => o.Name).Returns("Rumples");
            
            var returnedScore = -1;
            var game = new Game();
            game.Consensus += score => returnedScore = score;

            var james = new Player {Name = "James"};
            game.AddPlayer(james);
            game.Play(new Card { Player = james, Value = masterScore });

            var rumples = new Player { Name = "Rumples" };
            game.AddPlayer(rumples);

            gameRepositoryMock.Setup(g => g.Get(It.IsAny<int>())).Returns(game);
            
            var c = new StoryController(null, gameRepositoryMock.Object, gameStarterMock.Object);
            c.Vote(storyId, masterScore);

            Assert.That(returnedScore, Is.EqualTo(masterScore));
        }
    }
}