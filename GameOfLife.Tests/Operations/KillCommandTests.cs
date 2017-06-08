using GameOfLife.Operations;
using NUnit.Framework;

namespace GameOfLife.Tests.Operations
{
    [TestFixture]
    public class KillCommandTests : TestsUsingCell
    {
        private KillCommand _command;

        [SetUp]
        public new void SetUp()
        {
            _command = new KillCommand();
        }

        [Test]
        public void Execute_GivenCell_SetsCellAliveToFalse()
        {
            _command.Execute(Cell);

            Assert.That(Cell.Alive, Is.False);
        }
    }
}
