using System.Collections.Generic;
using System.Linq;
using static GameOfLife.LifeState;

namespace GameOfLife
{
    public class Rule : IRule
    {
        private uint[] _numbersYieldingLife;

        public Rule(uint[] numbersYieldingLife)
        {
            _numbersYieldingLife = numbersYieldingLife;
        }

        public LifeState Apply(IEnumerable<Cell> neighboringCells)
        {
            var liveCellCount = (uint)neighboringCells
                .Where(cell => cell.CurrentState == Alive)
                .Count();
            if (_numbersYieldingLife.Contains(liveCellCount))
                return Alive;
            return Dead;
        }
    }
}
