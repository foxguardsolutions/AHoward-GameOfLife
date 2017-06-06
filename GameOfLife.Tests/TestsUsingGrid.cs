using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLife.Tests
{
    [TestFixture]
    public class TestsUsingGrid : Tests
    {
        protected Grid Grid;

        [SetUp]
        public new void SetUp()
        {
            Grid = Fixture.Create<Grid>();
            InitializeGrid(Grid);
        }

        protected IEnumerable<Cell> FlattenCells(Cell[,] cells) => cells.Cast<Cell>();

        protected IEnumerable<bool> GetCellStatuses(Grid grid) => FlattenCells(grid.Cells).Select(c => c.Alive);

        protected static void InitializeGrid(Grid grid)
        {
            for (var row = 0; row < grid.Height; row++)
                for (var column = 0; column < grid.Width; column++)
                    grid.Cells[row, column] = new Cell(false);
        }
    }
}
