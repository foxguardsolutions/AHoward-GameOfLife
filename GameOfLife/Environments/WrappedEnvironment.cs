using System.Collections.Generic;

namespace GameOfLife.Environments
{
    public class WrappedEnvironment : CellEnvironment
    {
        public WrappedEnvironment(int row, int column, Grid grid) : base(row, column, grid) { }

        protected override void FindNeighbours(int row, int column, Grid grid)
        {
            var neighbours = new List<Cell>();

            for (var currentRow = row - 1; currentRow <= row + 1; currentRow++)
                for (var currentColumn = column - 1; currentColumn <= column + 1; currentColumn++)
                {
                    var columnInBounds = WrapColumnInBounds(currentColumn, grid);
                    var rowInBounds = WrapRowInBounds(currentRow, grid);
                    if (rowInBounds != row || columnInBounds != column)
                        neighbours.Add(grid.Cells[rowInBounds, columnInBounds]);
                }

            Neighbours = neighbours;
        }

        private int WrapColumnInBounds(int columnNumber, Grid grid)
        {
            var column = columnNumber;

            if (column < 0)
                column = grid.Width - 1;
            else if (column >= grid.Width)
                column = 0;

            return column;
        }

        private int WrapRowInBounds(int rowNumber, Grid grid)
        {
            var row = rowNumber;

            if (row < 0)
                row = grid.Height - 1;
            else if (row >= grid.Height)
                row = 0;

            return row;
        }
    }
}
