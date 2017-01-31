using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class SquareTileGridGetNeighborsOnSingleRowGridTests : BaseTests
    {
        private Cell[][] _cells;
        private CellPosition _positionAtOneEndOfRow;
        private CellPosition _positionInMiddleOfRow;

        [SetUp]
        public void SetUp()
        {
            var gridWidthNotIncludingEndCells = Fixture.Create<int>();
            var totalGridWidth = 1 + gridWidthNotIncludingEndCells + 1;
            _cells = Fixture.CreateRectangularJaggedArray<Cell>(1, totalGridWidth);

            var columnAtOneEndOfRow = Fixture.PickFromValues<uint>(0, (uint)totalGridWidth - 1);
            _positionAtOneEndOfRow = new CellPosition(0, columnAtOneEndOfRow);

            var columnInMiddleOfRow = Fixture.CreateInRange<uint>(1, totalGridWidth - 2);
            _positionInMiddleOfRow = new CellPosition(0, columnInMiddleOfRow);
        }

        [Test]
        public void GetNeighbors_GivenCellPositionAtEndOfSingleRowGridWithNoWrapping_YieldsOneNeighbor()
        {
            var grid = new SquareTileGrid(_cells, false, false);
            var cellAtEndOfRow = grid.GetCellAt(_positionAtOneEndOfRow);
            var neighborsOfCellAtEndOfRow = grid.GetNeighborsOfCellAt(_positionAtOneEndOfRow);

            Assert.That(neighborsOfCellAtEndOfRow, Has.Exactly(1).Items);
        }

        [Test]
        public void GetNeighbors_GivenCellPositionAtEndOfSingleRowGridThatWrapsOnlyOnTheSingleCellDimension_YieldsRowEndCellAndOneOtherNeighbor()
        {
            var grid = new SquareTileGrid(_cells, true, false);
            var cellAtEndOfRow = grid.GetCellAt(_positionAtOneEndOfRow);
            var neighborsOfCellAtEndOfRow = grid.GetNeighborsOfCellAt(_positionAtOneEndOfRow);

            Assert.That(neighborsOfCellAtEndOfRow, Has.Exactly(2).Items);
            Assert.That(neighborsOfCellAtEndOfRow, Has.Exactly(1).EqualTo(cellAtEndOfRow));
        }

        [Test]
        public void GetNeighbors_GivenCellPositionInTheMiddleOfSingleRowGridThatWrapsOnTheSingleCellDimension_YieldsMiddleCellAndTwoOtherNeighbors()
        {
            var grid = new SquareTileGrid(_cells, true, Fixture.Create<bool>());
            var cellInMiddleOfRow = grid.GetCellAt(_positionInMiddleOfRow);
            var neighborsOfCellInMiddleOfRow = grid.GetNeighborsOfCellAt(_positionInMiddleOfRow);

            Assert.That(neighborsOfCellInMiddleOfRow, Has.Exactly(3).Items);
            Assert.That(neighborsOfCellInMiddleOfRow, Has.Exactly(1).EqualTo(cellInMiddleOfRow));
        }
    }
}
