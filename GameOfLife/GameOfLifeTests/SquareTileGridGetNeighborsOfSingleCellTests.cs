using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class SquareTileGridGetNeighborsOfSingleCellTests : SquareTileGridTests
    {
        private LifeState[,] _seed;
        private CellPosition _onlyPositionOnTheGrid;

        [SetUp]
        public void SetUp()
        {
            _seed = new LifeState[1, 1];
            _seed[0, 0] = Fixture.Create<LifeState>();
            _onlyPositionOnTheGrid = new CellPosition(0, 0);
        }

        [Test]
        public void GetNeighbors_GivenSingleCellGridWithNoWrapping_YieldsNoNeighbors()
        {
            var grid = GridFactory.CreateSquareTileGrid(_seed);
            var neighborsOfSingleCell = grid.GetNeighborsOfCellAt(_onlyPositionOnTheGrid);

            Assert.That(neighborsOfSingleCell, Is.Empty);
        }

        [Test]
        public void GetNeighbors_GivenSingleCellGridThatWrapsOnOneDimension_YieldsSingleCellTwice()
        {
            var grid = GridFactory.CreateSquareTileGrid(_seed, true, false);
            var onlyCellInTheGrid = grid.GetCellAt(_onlyPositionOnTheGrid);
            var neighborsOfSingleCell = grid.GetNeighborsOfCellAt(_onlyPositionOnTheGrid);

            Assert.That(neighborsOfSingleCell, Has.Exactly(2).Items);
            Assert.That(neighborsOfSingleCell, Has.Exactly(2).EqualTo(onlyCellInTheGrid));
        }

        [Test]
        public void GetNeighbors_GivenSingleCellGridThatWrapsOnBothDimension_YieldsSingleCellFourTimes()
        {
            var grid = GridFactory.CreateSquareTileGrid(_seed, true, true);
            var onlyCellInTheGrid = grid.GetCellAt(_onlyPositionOnTheGrid);
            var singleGridCellFourTimes = new Cell[] { onlyCellInTheGrid, onlyCellInTheGrid, onlyCellInTheGrid, onlyCellInTheGrid };
            var neighborsOfSingleCell = grid.GetNeighborsOfCellAt(_onlyPositionOnTheGrid);

            Assert.That(neighborsOfSingleCell, Has.Exactly(4).Items);
            Assert.That(neighborsOfSingleCell, Has.Exactly(4).EqualTo(onlyCellInTheGrid));
        }
    }
}
