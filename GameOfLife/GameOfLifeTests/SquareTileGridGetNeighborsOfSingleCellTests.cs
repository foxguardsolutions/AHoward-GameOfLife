using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class SquareTileGridGetNeighborsOfSingleCellTests : BaseTests
    {
        private Cell[,] _cells;
        private CellPosition _onlyPositionOnTheGrid;

        [SetUp]
        public void SetUp()
        {
            _cells = new Cell[1, 1];
            _cells[0, 0] = Fixture.Create<Cell>();
            _onlyPositionOnTheGrid = new CellPosition(0, 0);
        }

        [Test]
        public void GetNeighbors_GivenSingleCellGridWithNoWrapping_YieldsNoNeighbors()
        {
            var grid = new SquareTileGrid(_cells, false, false);
            var neighborsOfSingleCell = grid.GetNeighborsOfCellAt(_onlyPositionOnTheGrid);

            Assert.That(neighborsOfSingleCell, Is.Empty);
        }

        [Test]
        public void GetNeighbors_GivenSingleCellGridThatWrapsOnOneDimension_YieldsSingleCell()
        {
            var grid = new SquareTileGrid(_cells, true, false);
            var onlyCellInTheGrid = grid.GetCellAt(_onlyPositionOnTheGrid);
            var neighborsOfSingleCell = grid.GetNeighborsOfCellAt(_onlyPositionOnTheGrid);

            Assert.That(neighborsOfSingleCell, Has.Exactly(1).Items);
            Assert.That(neighborsOfSingleCell, Has.Exactly(1).EqualTo(onlyCellInTheGrid));
        }

        [Test]
        public void GetNeighbors_GivenSingleCellGridThatWrapsOnBothDimension_YieldsSingleCell()
        {
            var grid = new SquareTileGrid(_cells, true, true);
            var onlyCellInTheGrid = grid.GetCellAt(_onlyPositionOnTheGrid);
            var neighborsOfSingleCell = grid.GetNeighborsOfCellAt(_onlyPositionOnTheGrid);

            Assert.That(neighborsOfSingleCell, Has.Exactly(1).Items);
            Assert.That(neighborsOfSingleCell, Has.Exactly(1).EqualTo(onlyCellInTheGrid));
        }
    }
}
