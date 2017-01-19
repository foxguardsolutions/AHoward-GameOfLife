using System.Linq;

namespace GameOfLife
{
    public class Rule : IRule
    {
        private uint[] _numbersYieldingLife;

        internal Rule(uint[] numbersYieldingLife)
        {
            _numbersYieldingLife = numbersYieldingLife.Distinct().ToArray();
        }

        public LifeState Apply(uint number)
        {
            if (_numbersYieldingLife.Contains(number))
                return LifeState.Alive;
            return LifeState.Dead;
        }
    }
}
