using System.Collections.Generic;

namespace GameOfLife
{
    public interface IGrid
    {
        LifeState[,] GetCurrentPattern();
        Cell GetCellAt(CellPosition cellPosition);
        IEnumerable<Cell> GetNeighborsOfCellAt(CellPosition centerCellPosition);
        IEnumerator<CellPosition> GetEnumerator();
    }
}
