using NUnit.Framework;
using Ploeh.AutoFixture;
using System.Linq;

namespace GameOfLife.Tests
{
    [TestFixture]
    public class GridFactoryTests : TestsUsingGrid
    {
        private int _columns;
        private int _rows;
        private string _cellsString;
        private string _gridString;

        [TestCaseSource(nameof(BooleanValues))]
        public void Parse_GivenGridString_ReturnsExpectedGrid(bool alive)
        {
            GivenGridString(alive);
            var expectedCellValues = Enumerable.Range(0, _rows * _columns).Select(c => alive);

            var actual = GridFactory.Parse(_gridString);

            Assert.That(actual.Height, Is.EqualTo(_rows));
            Assert.That(actual.Width, Is.EqualTo(_columns));
            Assert.That(GetCellStatuses(actual), Is.EqualTo(expectedCellValues));
        }

        [Test]
        public void ParseCells_GivenCellStringWithGrid_UpdatesGridCells()
        {
            GivenCellString(Grid);
            var expectedCellValues = Enumerable.Range(0, _rows * _columns).Select(c => true);

            GridFactory.ParseCells(_cellsString, Grid);

            Assert.That(GetCellStatuses(Grid), Is.EqualTo(expectedCellValues));
        }

        private void GivenDimensionsOfGrid()
        {
            _columns = Fixture.Create<int>();
            _rows = Fixture.Create<int>();
        }

        private void GivenGridString(bool alive)
        {
            GivenDimensionsOfGrid();
            var cellsText = alive ? Cell.AliveCellString : Cell.DeadCellString;
            var rowText = NewLine + string.Concat(Enumerable.Repeat(cellsText, _columns));
            _gridString = $"{_rows} {_columns}"
                + string.Concat(Enumerable.Repeat(rowText, _rows));
        }

        private void GivenCellString(Grid grid)
        {
            _rows = grid.Height;
            _columns = grid.Width;
            var rowText = NewLine
                + string.Concat(Enumerable.Repeat(Cell.AliveCellString, _columns));
            _cellsString = string.Concat(Enumerable.Repeat(rowText, _rows));
        }
    }
}
