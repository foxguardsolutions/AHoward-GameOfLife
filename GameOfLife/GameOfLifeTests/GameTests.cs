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
            new Game();
        }
    }
}
