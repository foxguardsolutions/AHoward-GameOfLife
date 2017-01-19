using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class Ruleset
    {
        private Dictionary<LifeState, IRule> _rules;

        public Ruleset()
        {
            _rules = new Dictionary<LifeState, IRule>();
        }

        public void Set(LifeState state, IRule newRule)
        {
            _rules[state] = newRule;
        }

        public LifeState Apply(LifeState state, uint number)
        {
            return _rules[state].Apply(number);
        }

        public bool IsComplete()
        {
            return ForEvery<LifeState>(_rules.ContainsKey);
        }

        private static bool ForEvery<EnumType>(Func<EnumType, bool> predicate)
        {
            var enumValues = (IEnumerable<EnumType>)Enum.GetValues(typeof(EnumType));
            return enumValues.All(predicate);
        }
    }
}
