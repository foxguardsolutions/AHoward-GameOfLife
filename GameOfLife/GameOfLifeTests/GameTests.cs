using GameOfLife;
using NUnit.Framework;

namespace GameOfLifeTests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void NewGame_CreatesGame()
        {
            var game = new Game();

            Assert.That(game, Is.TypeOf(typeof(Game)));
        }
    }
}
