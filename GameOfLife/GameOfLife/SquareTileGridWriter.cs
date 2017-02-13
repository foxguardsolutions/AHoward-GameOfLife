using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class SquareTileGridWriter : RectangularGridWriter
    {
        public SquareTileGridWriter(IConsoleWriter defaultWriter)
        {
            DefaultWriter = defaultWriter;
        }

        protected override void AppendPositionToBuilder(CellPosition position, IGrid grid, StringBuilder builder, Dictionary<LifeState, string> stateRepresentations)
        {
            var cellRepresentation = GetRepresentationOfStateOfCellAt(position, grid, stateRepresentations);
            builder.Append(cellRepresentation);
        }
    }
}
