namespace GameOfLife
{
    public class GridFactory : IGridFactory
    {
        public IGrid CreateSquareTileGrid(LifeState[,] seed)
        {
            return CreateSquareTileGrid(seed, false, false);
        }

        public IGrid CreateSquareTileGrid(LifeState[,] seed, bool wrapsOnRows, bool wrapsOnColumns)
        {
            var cells = InitializeCells(seed);
            return new SquareTileGrid(cells, wrapsOnRows, wrapsOnColumns);
        }

        public IGrid CreateHexTileGrid(LifeState[,] seed)
        {
            return CreateHexTileGrid(seed, false, false);
        }

        public IGrid CreateHexTileGrid(LifeState[,] seed, bool wrapsOnRows, bool wrapsOnColumns)
        {
            var cells = InitializeCells(seed);
            return new HexTileGrid(cells, wrapsOnRows, wrapsOnColumns);
        }

        public IGrid CreateDefaultGrid(ISettings settings)
        {
            return settings.GetDefaultGrid(this);
        }

        private static Cell[][] InitializeCells(LifeState[,] seed)
        {
            var cells = new Cell[seed.GetLength(0)][];
            for (uint rowNumber = 0; rowNumber < seed.GetLength(0); rowNumber++)
            {
                cells[rowNumber] = new Cell[seed.GetLength(1)];
                for (uint columnNumber = 0; columnNumber < seed.GetLength(1); columnNumber++)
                    cells[rowNumber][columnNumber] = new Cell(seed[rowNumber, columnNumber]);
            }

            return cells;
        }
    }
}
