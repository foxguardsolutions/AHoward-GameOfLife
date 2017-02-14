using System.Collections;
using System.Collections.Generic;

namespace GameOfLife
{
    public abstract class RectangularGrid : IGrid
    {
        private Cell[][] _cells;
        protected Dimension RowDimension { get; private set; }
        protected Dimension ColumnDimension { get; set; }

        public RectangularGrid(Cell[][] cells, bool wrapsOnRows, bool wrapsOnColumns)
        {
            _cells = cells;
            RowDimension = new Dimension((uint)cells.Length, wrapsOnRows);
        }

        public IEnumerable<LifeState> GetCurrentPattern()
        {
            var pattern = new List<LifeState>();
            foreach (var position in this)
                pattern.Add(GetCellAt(position).CurrentState);

            return pattern;
        }

        public IEnumerable<uint> GetDimensions()
        {
            var numberOfRows = RowDimension.Max - RowDimension.Min + 1;
            yield return numberOfRows;

            var numberOfColumns = ColumnDimension.Max - ColumnDimension.Min + 1;
            yield return numberOfColumns;
        }

        public Cell GetCellAt(CellPosition cellPosition)
        {
            return _cells[cellPosition.DimensionOne][cellPosition.DimensionTwo];
        }

        public IEnumerable<Cell> GetNeighborsOfCellAt(CellPosition centerCellPosition)
        {
            var neighborPositions = GetNeighborPositions(centerCellPosition);
            foreach (var position in neighborPositions)
                yield return GetCellAt(position);
        }

        private IEnumerable<CellPosition> GetNeighborPositions(CellPosition centerCellPosition)
        {
            foreach (var position in GetNeighborPositionsUnequalTo(centerCellPosition))
                yield return position;

            if (RowDimension.IsOwnNeighbor() || ColumnDimension.IsOwnNeighbor())
                yield return centerCellPosition;
        }

        protected abstract IEnumerable<CellPosition> GetNeighborPositionsUnequalTo(CellPosition centerCellPosition);

        public IEnumerator<CellPosition> GetEnumerator()
        {
            for (uint row = RowDimension.Min; row <= RowDimension.Max; row++)
            {
                for (uint column = ColumnDimension.Min; column <= ColumnDimension.Max; column++)
                    yield return new CellPosition(row, column);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
