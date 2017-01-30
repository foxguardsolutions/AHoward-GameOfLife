using System.Collections.Generic;

namespace GameOfLife
{
    public interface IGrid
    {
        IEnumerable<LifeState> GetCurrentPattern();
        void WritePatternToConsole(IEnumerable<LifeState> pattern, IConsoleWriter consoleWriter);
        Cell GetCellAt(CellPosition cellPosition);
        IEnumerable<Cell> GetNeighborsOfCellAt(CellPosition centerCellPosition);
        IEnumerator<CellPosition> GetEnumerator();
    }
}
