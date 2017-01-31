using System.Collections.Generic;
using GameOfLife;

namespace GameOfLifeTests
{
    public static class MockGrid
    {
        public static SquareTileGrid GridWithGlider
        {
            get
            {
                var cells = MakeDeadCells(5, 10);

                var liveCells = new List<Cell>()
                {
                    cells[0][4],
                    cells[1][2],
                    cells[1][4],
                    cells[2][3],
                    cells[2][4]
                };
                foreach (var cell in liveCells)
                    SetToAlive(cell);

                return new SquareTileGrid(cells, true, true);
            }
        }

        private static void SetToAlive(Cell cell)
        {
            cell.SetNextState(LifeState.Alive);
            cell.AdvanceState();
        }

        private static Cell[][] MakeDeadCells(uint numberOfRows, uint numberOfColumns)
        {
            var cells = new Cell[numberOfRows][];
            for (int row = 0; row < numberOfRows; row++)
            {
                cells[row] = new Cell[numberOfColumns];
                for (int column = 0; column < numberOfColumns; column++)
                    cells[row][column] = new Cell(LifeState.Dead);
            }

            return cells;
        }
    }
}
