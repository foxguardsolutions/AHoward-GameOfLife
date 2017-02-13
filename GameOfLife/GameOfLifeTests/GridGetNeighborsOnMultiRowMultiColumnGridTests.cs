using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public abstract class GridGetNeighborsOnMultiRowMultiColumnGridTests : BaseTests
    {
        protected Cell[][] Cells { get; private set; }

        protected uint RowOnOneEdgeOfGrid { get; private set; }
        protected uint ColumnOnOneEdgeOfGrid { get; private set; }

        protected uint RowNotOnEdgeOfGrid { get; private set; }
        protected uint ColumnNotOnEdgeOfGrid { get; private set; }

        [SetUp]
        public void SetUp()
        {
            var gridHeightNotIncludingEdgeRows = Fixture.Create<int>();
            var gridWidthNotIncludingEdgeColumns = Fixture.Create<int>();
            var totalGridHeight = 1 + gridHeightNotIncludingEdgeRows + 1;
            var totalGridWidth = 1 + gridWidthNotIncludingEdgeColumns + 1;

            Cells = Fixture.CreateRectangularJaggedArray<Cell>(totalGridHeight, totalGridWidth);
            Fixture.Register(() => Cells);

            RowOnOneEdgeOfGrid = Fixture.PickFromValues<uint>(0, (uint)totalGridHeight - 1);
            ColumnOnOneEdgeOfGrid = Fixture.PickFromValues<uint>(0, (uint)totalGridWidth - 1);

            RowNotOnEdgeOfGrid = (uint)Fixture.CreateInRange(1, totalGridHeight - 2);
            ColumnNotOnEdgeOfGrid = (uint)Fixture.CreateInRange(1, totalGridWidth - 2);
        }

        protected CellPosition GivenInteriorPosition()
        {
            return new CellPosition(RowNotOnEdgeOfGrid, ColumnNotOnEdgeOfGrid);
        }

        protected CellPosition GivenNonCornerPositionOnEdgeRow()
        {
            return new CellPosition(RowOnOneEdgeOfGrid, ColumnNotOnEdgeOfGrid);
        }
    }
}
