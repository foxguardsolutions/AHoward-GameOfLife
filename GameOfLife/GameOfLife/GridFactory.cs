using System;

namespace GameOfLife
{
    public class GridFactory : IGridFactory
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

        public SquareTileGrid CreateDefaultGrid()
        {
            var cells = InitializeCells(DefaultSettings.Seed);
            return new SquareTileGrid(cells, true, true);
        }

        public ToroidFriendlyHexTileGrid CreateHexTileGrid(LifeState[,] seed)
        {
            return CreateToroidFriendlyHexTileGrid(seed, false, false);
        }

        public ToroidFriendlyHexTileGrid CreateToroidFriendlyHexTileGrid(LifeState[,] seed, bool wrapsOnRows, bool wrapsOnColumns)
        {
            var cells = InitializeCells(seed);
            return new ToroidFriendlyHexTileGrid(cells, wrapsOnRows, wrapsOnColumns);
        }

        private Cell[][] InitializeCells(LifeState[,] seed)
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
