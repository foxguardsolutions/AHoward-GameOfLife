using GameOfLife;

namespace GameOfLifeTests
{
    public class HexTileGridGetNeighborsOnSingleRowGridTests : GridGetNeighborsOnSingleRowGridTests
    {
        protected override IGrid GivenNewGrid(Cell[][] cells, bool wrapsOnRows, bool wrapsOnColumns)
        {
            return new HexTileGrid(cells, wrapsOnRows, wrapsOnColumns);
        }
    }
}
