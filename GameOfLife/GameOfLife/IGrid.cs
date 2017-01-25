using System.Collections.Generic;

namespace GameOfLife
{
    public interface IGrid
    {
        Cell GetCellAt(CellPosition cellPosition);
        IEnumerable<Cell> GetNeighborsOfCellAt(CellPosition centerCellPosition);
        IEnumerator<CellPosition> GetEnumerator();
    }
}
