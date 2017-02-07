using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class SquareTileGridWriter : IGridWriter
    {
        public IConsoleWriter DefaultWriter { get; set; }

        public SquareTileGridWriter(IConsoleWriter defaultWriter)
        {
            DefaultWriter = defaultWriter;
        }

        public void WriteCurrentStateOf(IGrid grid)
        {
            var gridAsString = GridToString(grid);
            DefaultWriter.WriteLine(gridAsString);
        }

        public string GridToString(IGrid grid)
        {
            return GetRepresentationOf(grid, DefaultSettings.StateRepresentations);
        }

        private string GetRepresentationOf(IGrid grid, Dictionary<LifeState, string> stateRepresentations)
        {
            var builder = new StringBuilder();
            var column = 0;
            var columnsPerLine = grid.GetDimensions().Last();

            foreach (var position in grid)
            {
                builder.Append(GetRepresentationOfStateOfCellAt(position, grid, stateRepresentations));
                column++;
                column = WrapIfNeeded(builder, column, columnsPerLine);
            }

            return builder.ToString();
        }

        private string GetRepresentationOfStateOfCellAt(CellPosition position, IGrid grid, Dictionary<LifeState, string> stateMapping)
        {
            var cell = grid.GetCellAt(position);
            var state = cell.CurrentState;
            return stateMapping[state];
        }

        private static int WrapIfNeeded(StringBuilder builder, int column, uint columnsPerLine)
        {
            if (column == columnsPerLine)
            {
                builder.Append(Environment.NewLine);
                return 0;
            }

            return column;
        }
    }
}
