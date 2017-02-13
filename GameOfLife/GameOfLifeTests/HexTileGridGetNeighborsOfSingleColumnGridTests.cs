using System.Collections.Generic;
using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class HexTileGridGetNeighborsOfSingleColumnGridTests : BaseTests
    {
        private Cell[][] _singleColumnOfCells;
        private int _totalGridHeight;

        [SetUp]
        public void SetUp()
        {
            var gridHeightNotIncludingEndCells = Fixture.Create<int>();
            _totalGridHeight = 1 + gridHeightNotIncludingEndCells + 1;
            _singleColumnOfCells = Fixture.CreateRectangularJaggedArray<Cell>(_totalGridHeight, 1);
        }

        [TestCaseSource(nameof(EndOfColumnTestCases))]
        public void GetNeighbors_GivenCellPositionAtEndOfSingleColumnGrid(
            bool wrapsOnColumns, int totalNumberOfNeighbors, int numberOfNeighborsEqualToItself)
        {
            var grid = GivenNewGrid(_singleColumnOfCells, false, wrapsOnColumns);

            var rowAtOneEndOfColumn = Fixture.PickFromValues<uint>(0, (uint)_totalGridHeight - 1);
            var positionAtOneEndOfColumn = new CellPosition(rowAtOneEndOfColumn, 0);
            var cellAtEndOfColumn = grid.GetCellAt(positionAtOneEndOfColumn);

            var neighborsOfCellAtEndOfColumn = grid.GetNeighborsOfCellAt(positionAtOneEndOfColumn);

            Assert.That(
                neighborsOfCellAtEndOfColumn,
                Has.Exactly(totalNumberOfNeighbors).Items.And.Exactly(numberOfNeighborsEqualToItself).EqualTo(cellAtEndOfColumn));
        }

        public static IEnumerable<TestCaseData> EndOfColumnTestCases()
        {
            yield return new TestCaseData(false, 1, 0)
                .SetName("GetNeighbors_GivenCellPositionAtEndOfSingleColumnGridWithNoWrapping_YieldsOneNeighbor");

            yield return new TestCaseData(true, 2, 1)
               .SetName("GetNeighbors_GivenCellPositionAtEndOfSingleColumnGridThatWrapsOnlyOnTheSingleCellDimension_YieldsColumnEndCellAndOneOtherNeighbor");
        }

        [TestCaseSource(nameof(MiddleOfColumnTestCases))]
        public void GetNeighbors_GivenCellPositionInTheMiddleOfSingleColumnGrid(
            bool wrapsOnColumns, int totalNumberOfNeighbors, int numberOfNeighborsEqualToItself)
        {
            var grid = GivenNewGrid(_singleColumnOfCells, Fixture.Create<bool>(), wrapsOnColumns);

            var rowInMiddleOfColumn = (uint)Fixture.CreateInRange(1, _totalGridHeight - 2);
            var positionInMiddleOfColumn = new CellPosition(rowInMiddleOfColumn, 0);
            var cellInMiddleOfColumn = grid.GetCellAt(positionInMiddleOfColumn);

            var neighborsOfCellInMiddleOfRow = grid.GetNeighborsOfCellAt(positionInMiddleOfColumn);

            Assert.That(
                neighborsOfCellInMiddleOfRow,
                Has.Exactly(totalNumberOfNeighbors).Items.And.Exactly(numberOfNeighborsEqualToItself).EqualTo(cellInMiddleOfColumn));
        }

        public static IEnumerable<TestCaseData> MiddleOfColumnTestCases()
        {
            yield return new TestCaseData(false, 2, 0)
                .SetName("GetNeighbors_GivenCellPositionInTheMiddleOfSingleColumnGridThatDoesNotWrapOnTheSingleCellDimension_YieldsTwoNeighbors");

            yield return new TestCaseData(true, 3, 1)
               .SetName("GetNeighbors_GivenCellPositionInTheMiddleOfSingleColumnGridThatWrapsOnTheSingleCellDimension_YieldsMiddleCellAndTwoOtherNeighbors");
        }

        protected IGrid GivenNewGrid(Cell[][] cells, bool wrapsOnRows, bool wrapsOnColumns)
        {
            return new HexTileGrid(cells, wrapsOnRows, wrapsOnColumns);
        }
    }
}
