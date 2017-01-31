using System.Collections.Generic;

namespace GameOfLife
{
    public interface IGrid : IEnumerable<CellPosition>
    {
        IEnumerable<LifeState> GetCurrentPattern();
        void WritePatternToConsole(IEnumerable<LifeState> pattern, IConsole console);
        Cell GetCellAt(CellPosition cellPosition);
        IEnumerable<Cell> GetNeighborsOfCellAt(CellPosition centerCellPosition);
    }
}
