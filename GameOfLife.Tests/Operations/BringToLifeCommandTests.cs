using GameOfLife.Operations;
using NUnit.Framework;

namespace GameOfLife.Tests.Operations
{
    [TestFixture]
    public class BringToLifeCommandTests : TestsUsingCell
    {
        private BringToLifeCommand _command;

        [SetUp]
        public new void SetUp()
        {
            _command = new BringToLifeCommand();
        }

        [Test]
        public void Execute_GivenCell_SetsCellAliveToTrue()
        {
            _command.Execute(Cell);

            Assert.That(Cell.Alive, Is.True);
        }
    }
}
