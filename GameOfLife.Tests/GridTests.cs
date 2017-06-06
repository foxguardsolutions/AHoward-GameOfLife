using NUnit.Framework;

namespace GameOfLife.Tests
{
    [TestFixture]
    public class GridTests : TestsUsingGrid
    {
        [Test]
        public void ToString_GivenAlternatingGrid_ReturnsExpectedString()
        {
            GivenAlternatingGrid();
            var expected = GetExpectedAlternatingGridString();

            var actual = Grid.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ToString_GivenGrid_ReturnsHeightAndWidthOnFirstLine()
        {
            var expected = $"{Grid.Height} {Grid.Width}{NewLine}";

            var actual = Grid.ToString();

            Assert.That(actual.StartsWith(expected));
        }

        [TestCaseSource(nameof(BooleanValues))]
        public void ToString_GivenUniformGrid_ReturnsExpectedString(bool alive)
        {
            GivenUniformGrid(alive);
            var expected = GetExpectedUniformGridString(alive);

            var actual = Grid.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }

        private string GetExpectedAlternatingGridString()
        {
            var gridText = $"{Grid.Height} {Grid.Width}";

            for (var row = 0; row < Grid.Height; row++)
            {
                gridText += NewLine;

                for (var column = 0; column < Grid.Width; column++)
                    gridText += (row + column) % 2 == 0 ? AliveCellString : DeadCellString;
            }

            return gridText;
        }

        private string GetExpectedUniformGridString(bool alive)
        {
            var gridText = $"{Grid.Height} {Grid.Width}";

            for (var i = 0; i < Grid.Height; i++)
                gridText += NewLine.PadRight(NewLine.Length + Grid.Width,
                    alive ? AliveCellString[0] : DeadCellString[0]);

            return gridText;
        }

        private void GivenAlternatingGrid()
        {
            for (var row = 0; row < Grid.Height; row++)
                for (var column = 0; column < Grid.Width; column++)
                    Grid.Cells[row, column].Alive = (row + column) % 2 == 0;
        }

        private void GivenUniformGrid(bool alive)
        {
            foreach (var cell in Grid.Cells)
                cell.Alive = alive;
        }
    }
}
