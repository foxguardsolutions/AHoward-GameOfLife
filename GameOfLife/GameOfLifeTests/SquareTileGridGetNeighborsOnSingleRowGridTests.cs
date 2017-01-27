using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class SquareTileGridGetNeighborsOnSingleRowGridTests : SquareTileGridTests
    {
        private LifeState[,] _seed;
        private CellPosition _positionAtOneEndOfRow;
        private CellPosition _positionInMiddleOfRow;

        [SetUp]
        public void SetUp()
        {
            var gridWidthNotIncludingEndCells = Fixture.Create<int>();
            var totalGridWidth = 1 + gridWidthNotIncludingEndCells + 1;
            SetUpSeed(totalGridWidth);

            var columnAtOneEndOfRow = Fixture.PickFromValues<uint>(0, (uint)totalGridWidth - 1);
            _positionAtOneEndOfRow = new CellPosition(0, columnAtOneEndOfRow);

            var columnInMiddleOfRow = Fixture.CreateInRange<uint>(1, totalGridWidth - 2);
            _positionInMiddleOfRow = new CellPosition(0, columnInMiddleOfRow);
        }

        private void SetUpSeed(int gridWidth)
        {
            _seed = new LifeState[1, gridWidth];
            for (int i = 0; i < gridWidth; i++)
                _seed[0, i] = Fixture.Create<LifeState>();
        }

        [Test]
        public void GetNeighbors_GivenCellPositionAtEndOfSingleRowGridWithNoWrapping_YieldsOneNeighbor()
        {
            var grid = GridFactory.CreateSquareTileGrid(_seed, false, false);
            var cellAtEndOfRow = grid.GetCellAt(_positionAtOneEndOfRow);
            var neighborsOfCellAtEndOfRow = grid.GetNeighborsOfCellAt(_positionAtOneEndOfRow);

            Assert.That(neighborsOfCellAtEndOfRow, Has.Exactly(1).Items);
        }

        [Test]
        public void GetNeighbors_GivenCellPositionAtEndOfSingleRowGridThatWrapsOnlyOnTheSingleCellDimension_YieldsRowEndCellTwiceAndOneOtherNeighbor()
        {
            var grid = GridFactory.CreateSquareTileGrid(_seed, true, false);
            var cellAtEndOfRow = grid.GetCellAt(_positionAtOneEndOfRow);
            var neighborsOfCellAtEndOfRow = grid.GetNeighborsOfCellAt(_positionAtOneEndOfRow);

            Assert.That(neighborsOfCellAtEndOfRow, Has.Exactly(3).Items);
            Assert.That(neighborsOfCellAtEndOfRow, Has.Exactly(2).EqualTo(cellAtEndOfRow));
        }

        [Test]
        public void GetNeighbors_GivenCellPositionInTheMiddleOfSingleRowGridThatWrapsOnTheSingleCellDimension_YieldsMiddleCellTwiceAndTwoOtherNeighbors()
        {
            var grid = GridFactory.CreateSquareTileGrid(_seed, true, Fixture.Create<bool>());
            var cellInMiddleOfRow = grid.GetCellAt(_positionInMiddleOfRow);
            var neighborsOfCellInMiddleOfRow = grid.GetNeighborsOfCellAt(_positionInMiddleOfRow);

            Assert.That(neighborsOfCellInMiddleOfRow, Has.Exactly(4).Items);
            Assert.That(neighborsOfCellInMiddleOfRow, Has.Exactly(2).EqualTo(cellInMiddleOfRow));
        }
    }
}
