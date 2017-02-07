namespace GameOfLife
{
    public interface IGridFactory
    {
        SquareTileGrid CreateSquareTileGrid(LifeState[,] seed);
        SquareTileGrid CreateSquareTileGrid(LifeState[,] seed, bool wrapsOnRows, bool wrapsOnColumns);
        SquareTileGrid CreateDefaultGrid();
    }
}