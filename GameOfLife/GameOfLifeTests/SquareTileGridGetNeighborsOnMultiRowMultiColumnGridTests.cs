using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class SquareTileGridGetNeighborsOnMultiRowMultiColumnGridTests : SquareTileGridTests
    {
        private LifeState[,] _seed;

        private uint _rowOnOneEdgeOfGrid;
        private uint _columnOnOneEdgeOfGrid;

        private uint _rowNotOnEdgeOfGrid;
        private uint _columnNotOnEdgeOfGrid;

        [SetUp]
        public void SetUp()
        {
            var gridHeightNotIncludingEdgeRows = Fixture.Create<int>();
            var gridWidthNotIncludingEdgeColumns = Fixture.Create<int>();
            var totalGridHeight = 1 + gridHeightNotIncludingEdgeRows + 1;
            var totalGridWidth = 1 + gridWidthNotIncludingEdgeColumns + 1;
            SetUpSeed(totalGridHeight, totalGridWidth);

            _rowOnOneEdgeOfGrid = Fixture.PickFromValues<uint>(0, (uint)totalGridHeight - 1);
            _columnOnOneEdgeOfGrid = Fixture.PickFromValues<uint>(0, (uint)totalGridWidth - 1);

            _rowNotOnEdgeOfGrid = Fixture.CreateInRange<uint>(1, totalGridHeight - 2);
            _columnNotOnEdgeOfGrid = Fixture.CreateInRange<uint>(1, totalGridWidth - 2);
        }

        private void SetUpSeed(int gridHeight, int gridWidth)
        {
            _seed = new LifeState[gridHeight, gridWidth];
            for (int row = 0; row < gridHeight; row++)
            {
                for (int column = 0; column < gridWidth; column++)
                    _seed[row, column] = Fixture.Create<LifeState>();
            }
        }

        [Test]
        public void GetNeighbors_GivenCellPositionInCornerOfGridWithNoWrapping_YieldsThreeNeighbors()
        {
            var grid = GridFactory.CreateSquareTileGrid(_seed, false, false);
            var cornerPosition = new CellPosition(_rowOnOneEdgeOfGrid, _columnOnOneEdgeOfGrid);
            var cornerCell = grid.GetCellAt(cornerPosition);
            var neighborsOfCornerCell = grid.GetNeighborsOfCellAt(cornerPosition);

            Assert.That(neighborsOfCornerCell, Has.Exactly(3).Items);
        }

        [Test]
        public void GetNeighbors_GivenCellPositionOnEdgeOfGridWithNoWrappingOnThatEdge_YieldsFiveNeighbors()
        {
            var grid = GridFactory.CreateSquareTileGrid(_seed, false, Fixture.Create<bool>());
            var edgePosition = new CellPosition(_rowOnOneEdgeOfGrid, _columnNotOnEdgeOfGrid);
            var edgeCell = grid.GetCellAt(edgePosition);
            var neighborsOfEdgeCell = grid.GetNeighborsOfCellAt(edgePosition);

            Assert.That(neighborsOfEdgeCell, Has.Exactly(5).Items);
        }

        [Test]
        public void GetNeighbors_GivenCellPositionNotOnAGridEdge_YieldsEightNeighbors()
        {
            var grid = GridFactory.CreateSquareTileGrid(_seed, Fixture.Create<bool>(), Fixture.Create<bool>());
            var interiorPosition = new CellPosition(_rowNotOnEdgeOfGrid, _columnNotOnEdgeOfGrid);
            var interiorCell = grid.GetCellAt(interiorPosition);
            var neighborsOfInteriorCell = grid.GetNeighborsOfCellAt(interiorPosition);

            Assert.That(neighborsOfInteriorCell, Has.Exactly(8).Items);
        }
    }
}
