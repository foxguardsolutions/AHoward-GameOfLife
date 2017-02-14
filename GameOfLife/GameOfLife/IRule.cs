using System.Collections.Generic;

namespace GameOfLife
{
    public interface IRule
    {
        LifeState Apply(IEnumerable<Cell> neighboringCells);
    }
}
