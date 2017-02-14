using System.Collections.Generic;
using System.Linq;
using GameOfLife;
using NUnit.Framework;

namespace GameOfLifeTests
{
    public abstract class GridCellTests : BaseTests
    {
        protected Cell[][] Cells { get; private set; }
        protected IGrid Grid { get; set; }

        [SetUp]
        public void SetUpCells()
        {
            Cells = Fixture.CreateRectangularJaggedArray<Cell>();
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

        private IEnumerable<CellPosition> IterateOver(IGrid grid)
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
            var expectedPattern = new List<LifeState>();
            foreach (var row in Cells)
            {
                foreach (var cell in row)
                    expectedPattern.Add(cell.CurrentState);
            }

            var pattern = Grid.GetCurrentPattern();

            Assert.That(pattern, Is.EqualTo(expectedPattern));
        }

        [Test]
        public void GetDimensions_ReturnsDimensionsOfSeedCells()
        {
            var expectedNumberOfRows = (uint)Cells.Length;
            var expectedNumberOfColumns = (uint)Cells[0].Length;
            var expectedDimensions = new uint[] { expectedNumberOfRows, expectedNumberOfColumns };

            var dimensions = Grid.GetDimensions();

            Assert.That(dimensions, Is.EqualTo(expectedDimensions));
        }
    }
}
