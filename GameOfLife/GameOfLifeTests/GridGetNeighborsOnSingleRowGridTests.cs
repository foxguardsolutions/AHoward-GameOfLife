using System.Collections.Generic;
using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public abstract class GridGetNeighborsOnSingleRowGridTests : BaseTests
    {
        private Cell[][] _singleRowOfCells;
        private int _totalGridWidth;

        [SetUp]
        public void SetUp()
        {
            var gridWidthNotIncludingEndCells = Fixture.Create<int>();
            _totalGridWidth = 1 + gridWidthNotIncludingEndCells + 1;
            _singleRowOfCells = Fixture.CreateRectangularJaggedArray<Cell>(1, _totalGridWidth);
        }

        [TestCaseSource(nameof(EndOfRowTestCases))]
        public void GetNeighbors_GivenCellPositionAtEndOfSingleRowGrid(
            bool wrapsOnRows, int totalNumberOfNeighbors, int numberOfNeighborsEqualToItself)
        {
            var grid = GivenNewGrid(_singleRowOfCells, wrapsOnRows, false);

            var columnAtOneEndOfRow = Fixture.PickFromValues<uint>(0, (uint)_totalGridWidth - 1);
            var positionAtOneEndOfRow = new CellPosition(0, columnAtOneEndOfRow);
            var cellAtEndOfRow = grid.GetCellAt(positionAtOneEndOfRow);

            var neighborsOfCellAtEndOfRow = grid.GetNeighborsOfCellAt(positionAtOneEndOfRow);

            Assert.That(
                neighborsOfCellAtEndOfRow,
                Has.Exactly(totalNumberOfNeighbors).Items.And.Exactly(numberOfNeighborsEqualToItself).EqualTo(cellAtEndOfRow));
        }

        public static IEnumerable<TestCaseData> EndOfRowTestCases()
        {
            yield return new TestCaseData(false, 1, 0)
                .SetName("GetNeighbors_GivenCellPositionAtEndOfSingleRowGridWithNoWrapping_YieldsOneNeighbor");

            yield return new TestCaseData(true, 2, 1)
               .SetName("GetNeighbors_GivenCellPositionAtEndOfSingleRowGridThatWrapsOnlyOnTheSingleCellDimension_YieldsRowEndCellAndOneOtherNeighbor");
        }

        [TestCaseSource(nameof(MiddleOfRowTestCases))]
        public void GetNeighbors_GivenCellPositionInTheMiddleOfSingleRowGrid(
            bool wrapsOnRows, int totalNumberOfNeighbors, int numberOfNeighborsEqualToItself)
        {
            var grid = GivenNewGrid(_singleRowOfCells, wrapsOnRows, Fixture.Create<bool>());

            var columnInMiddleOfRow = (uint)Fixture.CreateInRange(1, _totalGridWidth - 2);
            var positionInMiddleOfRow = new CellPosition(0, columnInMiddleOfRow);
            var cellInMiddleOfRow = grid.GetCellAt(positionInMiddleOfRow);

            var neighborsOfCellInMiddleOfRow = grid.GetNeighborsOfCellAt(positionInMiddleOfRow);

            Assert.That(
                neighborsOfCellInMiddleOfRow,
                Has.Exactly(totalNumberOfNeighbors).Items.And.Exactly(numberOfNeighborsEqualToItself).EqualTo(cellInMiddleOfRow));
        }

        public static IEnumerable<TestCaseData> MiddleOfRowTestCases()
        {
            yield return new TestCaseData(false, 2, 0)
                .SetName("GetNeighbors_GivenCellPositionInTheMiddleOfSingleRowGridThatDoesNotWrapOnTheSingleCellDimension_YieldsTwoNeighbors");

            yield return new TestCaseData(true, 3, 1)
               .SetName("GetNeighbors_GivenCellPositionInTheMiddleOfSingleRowGridThatWrapsOnTheSingleCellDimension_YieldsMiddleCellAndTwoOtherNeighbors");
        }

        protected abstract IGrid GivenNewGrid(Cell[][] cells, bool wrapsOnRows, bool wrapsOnColumns);
    }
}
