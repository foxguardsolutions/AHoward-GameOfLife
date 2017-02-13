using System.Collections.Generic;

namespace GameOfLife
{
    public interface IRuleset
    {
        void SetRuleFor(LifeState state, params uint[] numbersYieldingLive);
        void SetDefaultRules(ISettings settings);
        bool IsComplete();
        void SetNextStateOfCellGivenNeighbors(Cell cell, IEnumerable<Cell> neighbors);
    }
}