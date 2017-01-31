using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class SquareTileGrid : IGrid
    {
        private Cell[][] _cells;
        private Dimension _rowDimension;
        private Dimension _columnDimension;

        public SquareTileGrid(Cell[][] cells, bool wrapsOnRows, bool wrapsOnColumns)
        {
            _cells = cells;
            _rowDimension = new Dimension((uint)cells.Length, wrapsOnRows);
            _columnDimension = new Dimension((uint)cells[0].Length, wrapsOnColumns);
        }

        public IEnumerable<IEnumerable<LifeState>> GetCurrentPattern()
        {
            var pattern = new LifeState[_cells.Length][];
            for (uint rowNumber = 0; rowNumber < _cells.Length; rowNumber++)
            {
                var row = GetRowPattern(rowNumber);
                yield return row;
            }
        }

        private IEnumerable<LifeState> GetRowPattern(uint rowNumber)
        {
            var cellsInRow = _cells[rowNumber].Length;
            var pattern = new LifeState[cellsInRow];
            for (uint columnNumber = 0; columnNumber < cellsInRow; columnNumber++)
                pattern[columnNumber] = GetCellAt(new CellPosition(rowNumber, columnNumber)).CurrentState;

            return pattern;
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
            var rowsInNeighborhood = _rowDimension.GetNeighborValues(centerCellPosition.DimensionOne);
            var columnsInNeighborhood = _columnDimension.GetNeighborValues(centerCellPosition.DimensionTwo);

            foreach (var position in GetPositionsExcluding(
                rowsInNeighborhood, columnsInNeighborhood, new CellPosition[] { centerCellPosition }))
                yield return position;

            if (_rowDimension.IsOwnNeighbor() || _columnDimension.IsOwnNeighbor())
                yield return centerCellPosition;
        }

        private IEnumerable<CellPosition> GetPositionsExcluding(
            IEnumerable<uint> rowNumbersToInclude, IEnumerable<uint> columnNumbersToInclude, IEnumerable<CellPosition> excludeCellPositions)
        {
            return rowNumbersToInclude.SelectMany(
                r => columnNumbersToInclude, (r, c) => new CellPosition(r, c))
                .Distinct()
                .Except(excludeCellPositions);
        }

        public IEnumerator<CellPosition> GetEnumerator()
        {
            for (uint row = _rowDimension.Min; row <= _rowDimension.Max; row++)
            {
                for (uint column = _columnDimension.Min; column <= _columnDimension.Max; column++)
                    yield return new CellPosition(row, column);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
