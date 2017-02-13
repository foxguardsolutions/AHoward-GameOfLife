using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class SquareTileGrid : RectangularGrid
    {
        public SquareTileGrid(Cell[][] cells, bool wrapsOnRows, bool wrapsOnColumns)
            : base(cells, wrapsOnRows, wrapsOnColumns)
        {
            ColumnDimension = new Dimension((uint)cells[0].Length, wrapsOnColumns);
        }

        protected override IEnumerable<CellPosition> GetNeighborPositionsUnequalTo(CellPosition centerCellPosition)
        {
            var rowsInNeighborhood = RowDimension.GetNeighborValues(centerCellPosition.DimensionOne);
            var columnsInNeighborhood = ColumnDimension.GetNeighborValues(centerCellPosition.DimensionTwo);

            var distinctNeighborPositions = GetDistinctPositionsExcluding(
                rowsInNeighborhood, columnsInNeighborhood, new CellPosition[] { centerCellPosition });

            foreach (var position in distinctNeighborPositions)
                yield return position;
        }

        private IEnumerable<CellPosition> GetDistinctPositionsExcluding(
            IEnumerable<uint> rowNumbersToInclude, IEnumerable<uint> columnNumbersToInclude, IEnumerable<CellPosition> excludeCellPositions)
        {
            return rowNumbersToInclude.SelectMany(
                r => columnNumbersToInclude, (r, c) => new CellPosition(r, c))
                .Distinct()
                .Except(excludeCellPositions);
        }
    }
}
