using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class HexTileGrid : RectangularGrid
    {
        public HexTileGrid(Cell[][] cells, bool wrapsOnRows, bool wrapsOnColumns)
            : base(cells, wrapsOnRows, wrapsOnColumns)
        {
            ColumnDimension = new OffsetDimension((uint)cells[0].Length, wrapsOnColumns);
        }

        protected override IEnumerable<CellPosition> GetNeighborPositionsUnequalTo(CellPosition centerCellPosition)
        {
            var distinctRowsInNeighborhood = RowDimension.GetNeighborValues(centerCellPosition.DimensionOne).Distinct();

            foreach (var row in distinctRowsInNeighborhood)
            {
                var distinctNeighboringPositions = GetNeighborsInRow(centerCellPosition, row).Distinct();

                foreach (var position in distinctNeighboringPositions)
                    yield return position;
            }
        }

        private IEnumerable<CellPosition> GetNeighborsInRow(CellPosition centerCellPosition, uint row)
        {
            if (row == centerCellPosition.DimensionOne)
            {
                foreach (var position in GetNeighborPositionsInSameRow(centerCellPosition))
                    yield return position;
            }
            else
            {
                foreach (var position in GetNeighborPositionsInNeighboringRow(centerCellPosition, row))
                    yield return position;
            }
        }

        private IEnumerable<CellPosition> GetNeighborPositionsInSameRow(CellPosition centerCellPosition)
        {
            var centerCellRow = centerCellPosition.DimensionOne;

            var neighboringColumns = ColumnDimension
                .GetNeighborValues(centerCellPosition.DimensionTwo)
                .Except(new uint[] { centerCellPosition.DimensionTwo });

            foreach (var neighboringColumn in neighboringColumns)
                yield return new CellPosition(centerCellRow, neighboringColumn);
        }

        private IEnumerable<CellPosition> GetNeighborPositionsInNeighboringRow(CellPosition centerCellPosition, uint row)
        {
            var neighboringColumns = GetNeighboringColumnsInNeighboringRowOf(centerCellPosition);

            foreach (var neighboringColumn in neighboringColumns)
                yield return new CellPosition(row, neighboringColumn);
        }

        private IEnumerable<uint> GetNeighboringColumnsInNeighboringRowOf(CellPosition centerCellPosition)
        {
            var centerCellIsInRowWithHighOffset = RowIsOffsetHigh(centerCellPosition);
            var columnDimension = GetOffsetColumnDimension();

            if (centerCellIsInRowWithHighOffset)
                return columnDimension.GetHighNeighborValues(centerCellPosition.DimensionTwo);
            return columnDimension.GetLowNeighborValues(centerCellPosition.DimensionTwo);
        }

        private bool RowIsOffsetHigh(CellPosition position)
        {
            return position.DimensionOne % 2 == 1;
        }

        private OffsetDimension GetOffsetColumnDimension()
        {
            return (OffsetDimension)ColumnDimension;
        }
    }
}
