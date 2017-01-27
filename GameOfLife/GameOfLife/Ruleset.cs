using System.Collections.Generic;
using static GameOfLife.EnumExtension;

namespace GameOfLife
{
    public class Ruleset : IRuleset
    {
        private Dictionary<LifeState, IRule> _rules;

        public Ruleset()
        {
            _rules = new Dictionary<LifeState, IRule>();
        }

        public IRule this[LifeState state]
        {
            set { _rules[state] = value; }
        }

        public void SetNextState(Cell cell, IEnumerable<Cell> neighbors)
        {
            var applicableRule = _rules[cell.CurrentState];
            cell.NextState = applicableRule.Apply(neighbors);
        }

        public bool IsComplete()
        {
            return ForEvery<LifeState>(_rules.ContainsKey);
        }
    }
}
