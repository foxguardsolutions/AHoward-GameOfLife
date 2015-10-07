using System.Collections.Generic;

namespace GameOfLife
{
    using NUnit.Framework;

    [TestFixture]
    public class CartesianGridLogicTest
    {
        private static readonly string[] ZeroNeighborsGrid = new[]
        {
            "...",
            "...",
            "..."
        };

        private static readonly string[] FourNeighborsGrid = new[]
        {
            "**.",
            "..*",
            ".*."
        };

        private static readonly string[] EightNeighborsGrid = new[]
        {
            "***",
            "*.*",
            "***"
        };

        [Test, TestCaseSource("NumberOfLiveNeighborsData")]
        public void TestNumberOfLiveNeighbors(string[] testArray, int expectedLiveNeighbors)
        {
            CartesianGrid grid = new CartesianGrid(testArray);

            CartesianGridLogic gridLogic = new CartesianGridLogic(grid);

            int liveNeighbors = gridLogic.NumberOfLiveNeighbors(grid.Map[1][1]);

            Assert.AreEqual(liveNeighbors, expectedLiveNeighbors);
        }

        [TestCase(0, 0, Result = true)]
        [TestCase(1, 0, Result = true)]
        [TestCase(2, 0, Result = false)]
        [TestCase(0, 1, Result = false)]
        [TestCase(2, 1, Result = true)]
        [TestCase(0, 2, Result = false)]
        [TestCase(1, 2, Result = true)]
        [TestCase(2, 2, Result = false)]
        public bool TestIsNeighborAlive(int x, int y)
        {
            CartesianGridCell gridCell = new CartesianGridCell(1, 1);

            CartesianGrid grid = new CartesianGrid(FourNeighborsGrid);

            CartesianGridLogic gridLogic = new CartesianGridLogic(grid);

            return gridLogic.IsNeighborAlive(gridCell, x, y);
        }

        [TestCase(0, 0, Result = true)]
        [TestCase(9, 9, Result = true)]
        [TestCase(3, 7, Result = true)]
        [TestCase(-1, 0, Result = false)]
        [TestCase(0, -1, Result = false)]
        [TestCase(10, 0, Result = false)]
        [TestCase(0, 10, Result = false)]
        public bool TestIsCellIndexValid(int x, int y)
        {
            CartesianGrid grid = new CartesianGrid(10, 10);

            CartesianGridLogic gridLogic = new CartesianGridLogic(grid);

            return gridLogic.IsCellIndexValid(x, y);
        }

        private static IEnumerable<TestCaseData> NumberOfLiveNeighborsData
        {
            get
            {
                yield return new TestCaseData(ZeroNeighborsGrid, 0);
                yield return new TestCaseData(FourNeighborsGrid, 4);
                yield return new TestCaseData(EightNeighborsGrid, 8);
            }
        }
    }
}
