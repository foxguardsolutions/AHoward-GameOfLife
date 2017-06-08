using GameOfLife.Operations;
using NUnit.Framework;

namespace GameOfLife.Tests.Operations
{
    [TestFixture]
    public class NoActionCommandTests : TestsUsingCell
    {
        private NoActionCommand _command;

        [SetUp]
        public new void SetUp()
        {
            _command = new NoActionCommand();
        }

        [Test]
        public void Execute_GivenCell_DoesNotChangeCellAliveStatus()
        {
            var expected = Cell.Alive;

            _command.Execute(Cell);

            Assert.That(Cell.Alive, Is.EqualTo(expected));
        }
    }
}
