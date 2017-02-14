using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class HexTileGridCellTests : GridCellTests
    {
        [SetUp]
        public void SetUpGrid()
        {
            Grid = new HexTileGrid(Cells, Fixture.Create<bool>(), Fixture.Create<bool>());
        }
    }
}
