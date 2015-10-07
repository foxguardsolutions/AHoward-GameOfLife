namespace GameOfLife
{
    using NUnit.Framework;

    [TestFixture]
    public class GridCellTest
    {
        private static object[] expectedNorthCells =
        {
            new object[] { 0, 0, null },
            new object[] { 3, 3, new GridCell(3, 2) },
            new object[] { 2, 1, new GridCell(2, 0) },
            new object[] { 9, 9, new GridCell(9, 8) }
        };

        [Test, TestCaseSource("expectedNorthCells")]
        public void TestCellToTheNorth(int x, int y, GridCell c)
        {
            Grid grid = new Grid(10, 10);

            GridCell actualCell = grid.Map[x][y].CellToTheNorth;

            Assert.AreEqual(c, actualCell);
        }

        private static object[] expectedEastCells =
        {
            new object[] { 0, 0, new GridCell(1, 0) },
            new object[] { 3, 3, new GridCell(4, 3) },
            new object[] { 2, 1, new GridCell(3, 1) },
            new object[] { 9, 9, null }
        };

        [Test, TestCaseSource("expectedEastCells")]
        public void TestCellToTheEast(int x, int y, GridCell c)
        {
            Grid grid = new Grid(10, 10);

            GridCell actualCell = grid.Map[x][y].CellToTheEast;

            Assert.AreEqual(c, actualCell);
        }

        private static object[] expectedSouthCells =
        {
            new object[] { 0, 0, new GridCell(0, 1) },
            new object[] { 3, 3, new GridCell(3, 4) },
            new object[] { 2, 1, new GridCell(2, 2) },
            new object[] { 9, 9, null }
        };

        [Test, TestCaseSource("expectedSouthCells")]
        public void TestCellToTheSouth(int x, int y, GridCell c)
        {
            Grid grid = new Grid(10, 10);

            GridCell actualCell = grid.Map[x][y].CellToTheSouth;

            Assert.AreEqual(c, actualCell);
        }

        private static object[] expectedWestCells =
        {
            new object[] { 0, 0, null },
            new object[] { 3, 3, new GridCell(2, 3) },
            new object[] { 2, 1, new GridCell(1, 1) },
            new object[] { 9, 9, new GridCell(8, 9) }
        };

        [Test, TestCaseSource("expectedWestCells")]
        public void TestCellToTheWest(int x, int y, GridCell c)
        {
            Grid grid = new Grid(10, 10);

            GridCell actualCell = grid.Map[x][y].CellToTheWest;

            Assert.AreEqual(c, actualCell);
        }

        private static object[] expectedNorthEastCells =
        {
            new object[] { 0, 0, null },
            new object[] { 3, 3, new GridCell(4, 2) },
            new object[] { 2, 1, new GridCell(3, 0) },
            new object[] { 9, 9, null }
        };

        [Test, TestCaseSource("expectedNorthEastCells")]
        public void TestCellToTheNorthEast(int x, int y, GridCell c)
        {
            Grid grid = new Grid(10, 10);

            GridCell actualCell = grid.Map[x][y].CellToTheNorthEast;

            Assert.AreEqual(c, actualCell);
        }

        private static object[] expectedNorthWestCells =
        {
            new object[] { 0, 0, null },
            new object[] { 3, 3, new GridCell(2, 2) },
            new object[] { 2, 1, new GridCell(1, 0) },
            new object[] { 9, 9, new GridCell(8, 8) }
        };

        [Test, TestCaseSource("expectedNorthWestCells")]
        public void TestCellToTheNorthWest(int x, int y, GridCell c)
        {
            Grid grid = new Grid(10, 10);

            GridCell actualCell = grid.Map[x][y].CellToTheNorthWest;

            Assert.AreEqual(c, actualCell);
        }

        private static object[] expectedSouthEastCells =
        {
            new object[] { 0, 0, new GridCell(1, 1) },
            new object[] { 3, 3, new GridCell(4, 4) },
            new object[] { 2, 1, new GridCell(3, 2) },
            new object[] { 9, 9, null }
        };

        [Test, TestCaseSource("expectedSouthEastCells")]
        public void TestCellToTheSouthEast(int x, int y, GridCell c)
        {
            Grid grid = new Grid(10, 10);

            GridCell actualCell = grid.Map[x][y].CellToTheSouthEast;

            Assert.AreEqual(c, actualCell);
        }

        private static object[] expectedSouthWestCells =
        {
            new object[] { 0, 0, null },
            new object[] { 3, 3, new GridCell(2, 4) },
            new object[] { 2, 1, new GridCell(1, 2) },
            new object[] { 9, 9, null }
        };

        [Test, TestCaseSource("expectedSouthWestCells")]
        public void TestCellToTheSouthWest(int x, int y, GridCell c)
        {
            Grid grid = new Grid(10, 10);

            GridCell actualCell = grid.Map[x][y].CellToTheSouthWest;

            Assert.AreEqual(c, actualCell);
        }

        [TestCase(11, 9, Result = 3)]
        [TestCase(11, 12, Result = 2)]
        public int TestNumberOfLiveNeighbors(int x, int y)
        {
            Grid grid = new Grid("../../smallexploder.txt");

            return grid.Map[x][y].NumberOfLiveNeighbors();
        }
    }
}
