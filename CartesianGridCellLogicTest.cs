namespace GameOfLife
{
    using NUnit.Framework;

    [TestFixture]
    public class CartesianGridCellLogicTest
    {
        [TestCase(false, 0, Result = false)]
        [TestCase(true, 0, Result = false)]
        [TestCase(false, 1, Result = false)]
        [TestCase(true, 1, Result = false)]
        [TestCase(false, 2, Result = false)]
        [TestCase(true, 2, Result = true)]
        [TestCase(false, 3, Result = true)]
        [TestCase(true, 3, Result = true)]
        [TestCase(false, 4, Result = false)]
        [TestCase(true, 4, Result = false)]
        public bool TestDetermineIfCellLives(bool isAlive, int liveNeighbors)
        {
            CartesianGridCell cell = new CartesianGridCell(0, 0, isAlive);

            CartesianGridCellLogic logic = new CartesianGridCellLogic();

            return logic.DetermineIfCellLives(cell, liveNeighbors);
        }

        [TestCase(0, Result = false)]
        [TestCase(1, Result = false)]
        [TestCase(2, Result = true)]
        [TestCase(3, Result = true)]
        [TestCase(4, Result = false)]
        public bool TestDoesLiveCellLive(int liveNeighbors)
        {
            CartesianGridCellLogic logic = new CartesianGridCellLogic();

            return logic.DoesLiveCellLive(liveNeighbors);
        }

        [TestCase(0, Result = false)]
        [TestCase(1, Result = false)]
        [TestCase(2, Result = false)]
        [TestCase(3, Result = true)]
        [TestCase(4, Result = false)]
        public bool TestDoesDeadCellComeAlive(int liveNeighbors)
        {
            CartesianGridCellLogic logic = new CartesianGridCellLogic();

            return logic.DoesDeadCellComeAlive(liveNeighbors);
        }
    }
}
