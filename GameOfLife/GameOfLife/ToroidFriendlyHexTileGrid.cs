using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class ToroidFriendlyHexTileGrid : IGrid
    {
        private Cell[,] _cells;
        private Dimension _offsetDimension; // Has to be even in number
        private Dimension _straightDimension;

        public ToroidFriendlyHexTileGrid(Cell[,] cells, bool wrapsOnOffsetDimension, bool wrapsOnStraightDimension)
        {
            _cells = cells;
            _offsetDimension = new Dimension((uint)cells.GetLength(0), wrapsOnOffsetDimension);
            _straightDimension = new Dimension((uint)cells.GetLength(1), wrapsOnStraightDimension);
        }

        public Cell GetCellAt(CellPosition cellPosition)
        {
            return _cells[cellPosition.DimensionOne, cellPosition.DimensionTwo];
        }

        public IEnumerable<LifeState> GetCurrentPattern()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<CellPosition> GetEnumerator()
        {
            for (uint row = _offsetDimension.Min; row <= _offsetDimension.Max; row++)
            {
                for (uint column = _straightDimension.Min; column <= _straightDimension.Max; column++)
                    yield return new CellPosition(row, column);
            }
        }

        public IEnumerable<Cell> GetNeighborsOfCellAt(CellPosition centerCellPosition)
        {
            throw new NotImplementedException();
        }

        public void WritePatternToConsole(IEnumerable<LifeState> pattern, IConsoleWriter consoleWriter)
        {
            throw new NotImplementedException();
        }
    }
}
