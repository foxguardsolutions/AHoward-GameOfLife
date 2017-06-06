using System;
using System.Linq;

namespace GameOfLife
{
    public static class GridFactory
    {
        internal const string NewLine = "\n";

        private const string CarriageReturn = "\r";
        private static readonly string[] Separators = { CarriageReturn, NewLine };

        public static Grid Parse(string gridString)
        {
            var tokens = GetTokens(gridString);
            var gridDimensions = ParseDimensions(tokens[0]);
            var cells = new Cell[gridDimensions[0], gridDimensions[1]];

            for (var i = 1; i < tokens.Length; i++)
                ParseCellRow(tokens[i], cells, i - 1);

            return new Grid(cells);
        }

        public static void ParseCells(string cellsString, Grid grid)
        {
            var rows = GetTokens(cellsString);
            
            for (var i = 0; i < rows.Length; i++)
                ParseCellRow(rows[i], grid.Cells, i);
        }

        private static string[] GetTokens(string gridString)
            => gridString.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

        private static void ParseCellRow(string rowString, Cell[,] cells, int rowNumber)
        {
            for (var i = 0; i < rowString.Length; i++)
            {
                if (Cell.TryParse(rowString.Substring(i, 1), out Cell cell))
                    cells[rowNumber, i] = cell;
                else
                    cells[rowNumber, i] = new Cell(false);
            }
        }

        private static int[] ParseDimensions(string gridString)
            => gridString.Split(' ').Select(int.Parse).ToArray();
    }
}
