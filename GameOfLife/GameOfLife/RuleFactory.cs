using System.Linq;

namespace GameOfLife
{
    public class RuleFactory : IRuleFactory
    {
        public IRule CreateRule(params uint[] neighborCountsYieldingLive)
        {
            return new Rule(neighborCountsYieldingLive.Distinct().ToArray());
        }
    }
}
