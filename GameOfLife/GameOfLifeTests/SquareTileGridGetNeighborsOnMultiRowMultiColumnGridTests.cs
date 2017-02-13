using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class SquareTileGridGetNeighborsOnMultiRowMultiColumnGridTests : GridGetNeighborsOnMultiRowMultiColumnGridTests
    {
        [Test]
        public void GetNeighbors_GivenCellPositionInCornerOfGridWithNoWrapping_YieldsThreeNeighbors()
        {
            var grid = GivenSquareTileGridWithNoWrapping();
            var cornerPosition = new CellPosition(RowOnOneEdgeOfGrid, ColumnOnOneEdgeOfGrid);
            var cornerCell = grid.GetCellAt(cornerPosition);
            var neighborsOfCornerCell = grid.GetNeighborsOfCellAt(cornerPosition);

            Assert.That(neighborsOfCornerCell, Has.Exactly(3).Items);
        }

        [Test]
        public void GetNeighbors_GivenCellPositionOnEdgeOfGridWithNoWrappingOnThatEdge_YieldsFiveNeighbors()
        {
            var grid = new SquareTileGrid(Cells, false, Fixture.Create<bool>());
            var edgePosition = GivenNonCornerPositionOnEdgeRow();
            var edgeCell = grid.GetCellAt(edgePosition);
            var neighborsOfEdgeCell = grid.GetNeighborsOfCellAt(edgePosition);

            Assert.That(neighborsOfEdgeCell, Has.Exactly(5).Items);
        }

        [Test]
        public void GetNeighbors_GivenCellPositionNotOnAGridEdge_YieldsEightNeighbors()
        {
            var grid = GivenSquareTileGrid();
            var interiorPosition = GivenInteriorPosition();
            var interiorCell = grid.GetCellAt(interiorPosition);
            var neighborsOfInteriorCell = grid.GetNeighborsOfCellAt(interiorPosition);

            Assert.That(neighborsOfInteriorCell, Has.Exactly(8).Items);
        }

        private IGrid GivenSquareTileGrid()
        {
            return Fixture.Create<SquareTileGrid>();
        }

        private IGrid GivenSquareTileGridWithNoWrapping()
        {
            Fixture.Register(() => false);
            return Fixture.Create<SquareTileGrid>();
        }
    }
}
