namespace GameOfLife
{
    public abstract class HexTileGridSettings : Settings
    {
        public IGrid GetDefaultGrid(IGridFactory gridFactory)
        {
            return gridFactory.CreateHexTileGrid(DefaultSeed, true, true);
        }
    }
}
