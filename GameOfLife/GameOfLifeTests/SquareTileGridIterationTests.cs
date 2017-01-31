using System.Collections.Generic;
using System.Linq;
using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class SquareTileGridIterationTests : BaseTests
    {
        protected Cell[][] Cells { get; private set; }
        protected SquareTileGrid Grid { get; private set; }

        [SetUp]
        public void SetUp()
        {
            Cells = Fixture.CreateRectangularJaggedArray<Cell>();
            Grid = new SquareTileGrid(Cells, Fixture.Create<bool>(), Fixture.Create<bool>());
        }

        [Test]
        public void IterationOverGrid_YieldsPositionsMatchingAllPossibleSeedIndices()
        {
            var allPossibleCellIndices = GetCartesianProductOfPositionsIn(Cells);
            var resultOfIteration = IterateOver(Grid);

            Assert.That(resultOfIteration, Is.EquivalentTo(allPossibleCellIndices));
        }

        [Test]
        public void GetCellAt_GivenCellPosition_ReturnsCellWithMatchingIndicesInCellArray()
        {
            var allPositions = IterateOver(Grid).ToArray();
            var position = Fixture.PickFromValues(allPositions);
            var cellInInputArray = Cells[position.DimensionOne][position.DimensionTwo];
            var cellAtPosition = Grid.GetCellAt(position);

            Assert.That(cellAtPosition, Is.EqualTo(cellInInputArray));
        }

        private IEnumerable<CellPosition> IterateOver(SquareTileGrid grid)
        {
            foreach (var position in grid)
                yield return position;
        }

        private IEnumerable<CellPosition> GetCartesianProductOfPositionsIn<T>(T[][] array)
        {
            for (uint i = 0; i < array.Length; i++)
            {
                for (uint j = 0; j < array[i].Length; j++)
                    yield return new CellPosition(i, j);
            }
        }

        [Test]
        public void GetCurrentPattern_WithoutAnyCellChanges_ReturnsPatternMatchingSeedPattern()
        {
            var expectedPattern = new LifeState[Cells.Length][];
            for (int rowNumber = 0; rowNumber < Cells.Length; rowNumber++)
            {
                expectedPattern[rowNumber] = new LifeState[Cells[rowNumber].Length];
                for (int columnNumber = 0; columnNumber < Cells[rowNumber].Length; columnNumber++)
                    expectedPattern[rowNumber][columnNumber] = Cells[rowNumber][columnNumber].CurrentState;
            }

            var pattern = Grid.GetCurrentPattern();

            Assert.That(pattern, Is.EqualTo(expectedPattern));
        }
    }
}
