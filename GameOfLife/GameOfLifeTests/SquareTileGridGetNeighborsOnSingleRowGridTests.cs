using GameOfLife;

namespace GameOfLifeTests
{
    public class SquareTileGridGetNeighborsOnSingleRowGridTests : GridGetNeighborsOnSingleRowGridTests
    {
        protected override IGrid GivenNewGrid(Cell[][] cells, bool wrapsOnRows, bool wrapsOnColumns)
        {
            return new SquareTileGrid(cells, wrapsOnRows, wrapsOnColumns);
        }
    }
}
