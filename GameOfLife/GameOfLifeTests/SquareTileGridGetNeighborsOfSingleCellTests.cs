using GameOfLife;

namespace GameOfLifeTests
{
    public class SquareTileGridGetNeighborsOfSingleCellTests : GridGetNeighborsOfSingleCellTests
    {
        protected override IGrid GivenNewGrid(Cell[][] cells, bool wrapsOnRows, bool wrapsOnColumns)
        {
            return new SquareTileGrid(cells, wrapsOnRows, wrapsOnColumns);
        }
    }
}
