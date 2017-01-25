namespace GameOfLife
{
    public class GridFactory
    {
        public SquareTileGrid CreateSquareTileGrid(LifeState[,] seed)
        {
            return CreateSquareTileGrid(seed, false, false);
        }

        public SquareTileGrid CreateSquareTileGrid(LifeState[,] seed, bool wrapsOnRows, bool wrapsOnColumns)
        {
            var cells = InitializeCells(seed);
            return new SquareTileGrid(cells, wrapsOnRows, wrapsOnColumns);
        }

        private Cell[,] InitializeCells(LifeState[,] seed)
        {
            var cells = new Cell[seed.GetLength(0), seed.GetLength(1)];
            for (uint rowNumber = 0; rowNumber < seed.GetLength(0); rowNumber++)
            {
                for (uint columnNumber = 0; columnNumber < seed.GetLength(1); columnNumber++)
                {
                    cells[rowNumber, columnNumber] = new Cell(seed[rowNumber, columnNumber]);
                }
            }

            return cells;
        }
    }
}
