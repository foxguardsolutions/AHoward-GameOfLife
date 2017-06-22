using System.Collections.Generic;

namespace GameOfLife.Environments
{
    public abstract class CellEnvironment
    {
        public Cell Cell { get; private set; }
        public IEnumerable<Cell> Neighbours { get; protected set; }

        protected CellEnvironment(int row, int column, Grid grid)
        {
            Cell = grid.Cells[row, column];
            FindNeighbours(row, column, grid);
        }

        protected abstract void FindNeighbours(int row, int column, Grid grid);
    }
}
