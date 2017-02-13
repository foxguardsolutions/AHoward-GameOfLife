using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public abstract class GridGetNeighborsOfSingleCellTests : BaseTests
    {
        private Cell[][] _singleCell;
        private CellPosition _onlyPositionOnTheGrid;

        [SetUp]
        public void SetUp()
        {
            _singleCell = Fixture.CreateRectangularJaggedArray<Cell>(1, 1);
            _onlyPositionOnTheGrid = new CellPosition(0, 0);
        }

        [Test]
        public void GetNeighbors_GivenSingleCellGridWithNoWrapping_YieldsNoNeighbors()
        {
            var grid = GivenNewGrid(_singleCell, false, false);
            var neighborsOfSingleCell = grid.GetNeighborsOfCellAt(_onlyPositionOnTheGrid);

            Assert.That(neighborsOfSingleCell, Is.Empty);
        }

        [Test]
        public void GetNeighbors_GivenSingleCellGridThatWrapsOnRowDimension_YieldsSingleCell()
        {
            var grid = GivenNewGrid(_singleCell, true, Fixture.Create<bool>());
            var onlyCellInTheGrid = grid.GetCellAt(_onlyPositionOnTheGrid);
            var neighborsOfSingleCell = grid.GetNeighborsOfCellAt(_onlyPositionOnTheGrid);

            Assert.That(neighborsOfSingleCell, Has.Exactly(1).Items.And.Exactly(1).EqualTo(onlyCellInTheGrid));
        }

        [Test]
        public void GetNeighbors_GivenSingleCellGridThatWrapsOnColumnDimension_YieldsSingleCell()
        {
            var grid = GivenNewGrid(_singleCell, Fixture.Create<bool>(), true);
            var onlyCellInTheGrid = grid.GetCellAt(_onlyPositionOnTheGrid);
            var neighborsOfSingleCell = grid.GetNeighborsOfCellAt(_onlyPositionOnTheGrid);

            Assert.That(neighborsOfSingleCell, Has.Exactly(1).Items.And.Exactly(1).EqualTo(onlyCellInTheGrid));
        }

        protected abstract IGrid GivenNewGrid(Cell[][] cells, bool wrapsOnRows, bool wrapsOnColumns);
    }
}
