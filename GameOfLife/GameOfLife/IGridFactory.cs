namespace GameOfLife
{
    public interface IGridFactory
    {
        IGrid CreateSquareTileGrid(LifeState[,] seed);
        IGrid CreateSquareTileGrid(LifeState[,] seed, bool wrapsOnRows, bool wrapsOnColumns);
        IGrid CreateHexTileGrid(LifeState[,] seed);
        IGrid CreateHexTileGrid(LifeState[,] seed, bool wrapsOnRows, bool wrapsOnColumns);
        IGrid CreateDefaultGrid(ISettings settings);
    }
}