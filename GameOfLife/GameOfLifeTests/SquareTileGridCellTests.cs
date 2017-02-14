using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class SquareTileGridCellTests : GridCellTests
    {
        [SetUp]
        public void SetUpGrid()
        {
            Grid = new SquareTileGrid(Cells, Fixture.Create<bool>(), Fixture.Create<bool>());
        }
    }
}
