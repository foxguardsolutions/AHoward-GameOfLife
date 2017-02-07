using System.Collections.Generic;
using static GameOfLife.EnumExtension;

namespace GameOfLife
{
    public class Ruleset : IRuleset
    {
        private IRuleFactory _ruleFactory;
        public Dictionary<LifeState, IRule> Rules { get; private set; }

        public Ruleset(IRuleFactory ruleFactory)
        {
            _ruleFactory = ruleFactory;
            Rules = new Dictionary<LifeState, IRule>();
        }

        public void SetRuleFor(LifeState state, params uint[] numbersYieldingLive)
        {
            var newRule = _ruleFactory.CreateRule(numbersYieldingLive);
            Rules[state] = newRule;
        }

        public void SetDefaultRules()
        {
            SetRuleFor(LifeState.Alive, DefaultSettings.SurvivalNumbers);
            SetRuleFor(LifeState.Dead, DefaultSettings.ReproductionNumbers);
        }

        public bool IsComplete()
        {
            return ForEvery<LifeState>(Rules.ContainsKey);
        }

        public void SetNextStateOfCellGivenNeighbors(Cell cell, IEnumerable<Cell> neighbors)
        {
            var applicableRule = Rules[cell.CurrentState];
            var newState = applicableRule.Apply(neighbors);
            cell.SetNextState(newState);
        }
    }
}
