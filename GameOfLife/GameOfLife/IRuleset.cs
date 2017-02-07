using System.Collections.Generic;

namespace GameOfLife
{
    public interface IRuleset
    {
        void SetRuleFor(LifeState state, params uint[] numbersYieldingLive);
        void SetDefaultRules();
        bool IsComplete();
        void SetNextStateOfCellGivenNeighbors(Cell cell, IEnumerable<Cell> neighbors);
    }
}