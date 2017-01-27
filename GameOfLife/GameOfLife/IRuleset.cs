using System.Collections.Generic;

namespace GameOfLife
{
    public interface IRuleset
    {
        IRule this[LifeState state] { set; }

        bool IsComplete();
        void SetNextState(Cell cell, IEnumerable<Cell> neighbors);
    }
}