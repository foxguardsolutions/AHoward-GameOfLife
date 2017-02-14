using System.Collections.Generic;
using System.Linq;
using static GameOfLife.LifeState;

namespace GameOfLife
{
    public class Rule : IRule
    {
        public uint[] NumbersYieldingLife { get; private set; }

        public Rule(uint[] numbersYieldingLife)
        {
            NumbersYieldingLife = numbersYieldingLife;
        }

        public LifeState Apply(IEnumerable<Cell> neighboringCells)
        {
            var liveCellCount = (uint)neighboringCells
                .Where(cell => cell.CurrentState == Alive)
                .Count();
            if (NumbersYieldingLife.Contains(liveCellCount))
                return Alive;
            return Dead;
        }
    }
}
