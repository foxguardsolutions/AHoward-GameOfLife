using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public abstract class RectangularGridWriter : IGridWriter
    {
        public IConsoleWriter DefaultWriter { get; set; }

        public void WriteCurrentStateOf(IGrid grid, Dictionary<LifeState, string> stateRepresentations)
        {
            var gridAsString = GridToString(grid, stateRepresentations);
            DefaultWriter.WriteLine(gridAsString);
        }

        private string GridToString(IGrid grid, Dictionary<LifeState, string> stateRepresentations)
        {
            return GetRepresentationOf(grid, stateRepresentations);
        }

        private string GetRepresentationOf(IGrid grid, Dictionary<LifeState, string> stateRepresentations)
        {
            var builder = new StringBuilder();
            var column = 0;
            var columnsPerLine = grid.GetDimensions().Last();

            foreach (var position in grid)
            {
                AppendPositionToBuilder(position, grid, builder, stateRepresentations);
                column++;
                column = WrapIfNeeded(builder, column, columnsPerLine);
            }

            return builder.ToString();
        }

        protected abstract void AppendPositionToBuilder(CellPosition position, IGrid grid, StringBuilder builder, Dictionary<LifeState, string> stateRepresentations);

        protected string GetRepresentationOfStateOfCellAt(CellPosition position, IGrid grid, Dictionary<LifeState, string> stateMapping)
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