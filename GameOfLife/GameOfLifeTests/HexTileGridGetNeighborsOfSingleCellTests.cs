using GameOfLife;

namespace GameOfLifeTests
{
    public class HexTileGridGetNeighborsOfSingleCellTests : GridGetNeighborsOfSingleCellTests
    {
        protected override IGrid GivenNewGrid(Cell[][] cells, bool wrapsOnRows, bool wrapsOnColumns)
        {
            return new HexTileGrid(cells, wrapsOnRows, wrapsOnColumns);
        }
    }
}
