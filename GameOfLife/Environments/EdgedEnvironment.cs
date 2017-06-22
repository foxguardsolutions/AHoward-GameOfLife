using System.Collections.Generic;

namespace GameOfLife.Environments
{
    public class EdgedEnvironment : CellEnvironment
    {
        public EdgedEnvironment(int row, int column, Grid grid) : base(row, column, grid) { }

        protected override void FindNeighbours(int row, int column, Grid grid)
        {
            var neighbours = new List<Cell>();

            for (var currentRow = row - 1; currentRow <= row + 1; currentRow++)
                for (var currentColumn = column - 1; currentColumn <= column + 1; currentColumn++)
                    if (IsInBounds(currentRow, currentColumn, grid) && !(currentRow == row && currentColumn == column))
                        neighbours.Add(grid.Cells[currentRow, currentColumn]);

            Neighbours = neighbours;
        }

        private bool IsInBounds(int row, int column, Grid grid)
        {
            return (0 <= row && row < grid.Height)
                && (0 <= column && column < grid.Width);
        }
    }
}
