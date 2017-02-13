using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class HexTileGridWriter : RectangularGridWriter
    {
        public HexTileGridWriter(IConsoleWriter defaultWriter)
        {
            DefaultWriter = defaultWriter;
        }

        protected override void AppendPositionToBuilder(CellPosition position, IGrid grid, StringBuilder builder, Dictionary<LifeState, string> stateRepresentations)
        {
            if (!IsAtBeginningOfAnEvenRow(position))
                builder.Append(" ");

            var cellRepresentation = GetRepresentationOfStateOfCellAt(position, grid, stateRepresentations);
            builder.Append(cellRepresentation);
        }

        private bool IsAtBeginningOfAnEvenRow(CellPosition position)
        {
            var isAtBeginningOfARow = position.DimensionTwo == 0;
            if (!isAtBeginningOfARow)
                return false;

            var rowNumberIsOdd = position.DimensionOne % 2 == 1;
            if (rowNumberIsOdd)
                return false;

            return true;
        }

        private string PadAtBeginningWithASpace(string valueToBePadded)
        {
            return " " + valueToBePadded;
        }
    }
}
