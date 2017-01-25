using System.Collections.Generic;

namespace GameOfLife
{
    public class SquareTileGrid : IGrid
    {
        private Cell[,] _cells;
        private Dimension _rowDimension;
        private Dimension _columnDimension;

        internal SquareTileGrid(Cell[,] cells, bool wrapsOnRows, bool wrapsOnColumns)
        {
            _cells = cells;
            _rowDimension = new Dimension((uint)cells.GetLength(0), wrapsOnRows);
            _columnDimension = new Dimension((uint)cells.GetLength(1), wrapsOnColumns);
        }

        public Cell GetCellAt(CellPosition cellPosition)
        {
            return _cells[cellPosition.DimensionOne, cellPosition.DimensionTwo];
        }

        public IEnumerable<Cell> GetNeighborsOfCellAt(CellPosition centerCellPosition)
        {
            var neighborPositions = GetNeighborPositions(centerCellPosition);
            foreach (var position in neighborPositions)
                yield return GetCellAt(position);
        }

        private IEnumerable<CellPosition> GetNeighborPositions(CellPosition centerCellPosition)
        {
            var rowsInNeighborhood = _rowDimension.GetNeighborValues(centerCellPosition.DimensionOne);
            var columnsInNeighborhood = _columnDimension.GetNeighborValues(centerCellPosition.DimensionTwo);
            foreach (var position in GetPositionsExcluding(rowsInNeighborhood, columnsInNeighborhood, centerCellPosition))
                yield return position;

            if (_rowDimension.IsOwnNeighbor())
            {
                yield return centerCellPosition;
                yield return centerCellPosition;
            }

            if (_columnDimension.IsOwnNeighbor())
            {
                yield return centerCellPosition;
                yield return centerCellPosition;
            }
        }

        private IEnumerable<CellPosition> GetPositionsExcluding(
            IEnumerable<uint> rowNumbersToInclude, IEnumerable<uint> columnNumbersToInclude, CellPosition excludeCellPosition)
        {
            foreach (var rowNumber in rowNumbersToInclude)
            {
                foreach (var columnNumber in columnNumbersToInclude)
                {
                    if (rowNumber != excludeCellPosition.DimensionOne || columnNumber != excludeCellPosition.DimensionTwo)
                        yield return new CellPosition(rowNumber, columnNumber);
                }
            }
        }

        public IEnumerator<CellPosition> GetEnumerator()
        {
            for (uint row = _rowDimension.Min; row <= _rowDimension.Max; row++)
            {
                for (uint column = _columnDimension.Min; column <= _columnDimension.Max; column++)
                    yield return new CellPosition(row, column);
            }
        }
    }
}
