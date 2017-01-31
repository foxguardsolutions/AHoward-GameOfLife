using System.Collections.Generic;

namespace GameOfLife
{
    public interface IGrid : IEnumerable<CellPosition>
    {
        IEnumerable<IEnumerable<LifeState>> GetCurrentPattern();
        Cell GetCellAt(CellPosition cellPosition);
        IEnumerable<Cell> GetNeighborsOfCellAt(CellPosition centerCellPosition);
    }
}
