using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLife.Tests
{
    [TestFixture]
    public class CellTests : TestsUsingCell
    {
        [Test]
        public void ToString_GivenDeadCell_ReturnsDeadCellString()
        {
            GivenDeadCell();
            var expected = DeadCellString;

            var actual = Cell.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ToString_GivenLiveCell_RetrunsAliveCellString()
        {
            GivenLiveCell();
            var expected = AliveCellString;

            var actual = Cell.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TryParse_GivenAliveCellString_CreatesLiveCell()
        {
            var actual = Cell.TryParse(AliveCellString, out Cell cell);

            Assert.That(actual, Is.True);
            Assert.That(cell.Alive, Is.True);
        }

        [Test]
        public void TryParse_GivenDeadCellString_CreatesDeadCell()
        {
            var actual = Cell.TryParse(DeadCellString, out Cell cell);

            Assert.That(actual, Is.True);
            Assert.That(cell.Alive, Is.False);
        }

        [Test]
        public void TryParse_GivenNonCellString_DoesNotCreateCell()
        {
            var str = Fixture.Create<string>();

            var actual = Cell.TryParse(str, out Cell cell);

            Assert.That(actual, Is.False);
            Assert.That(cell, Is.Null);
        }
    }
}
