using System.Linq;

namespace GameOfLife
{
    public class RuleFactory : IRuleFactory
    {
        public IRule Create(params uint[] neighborCountsYieldingLive)
        {
            return new Rule(neighborCountsYieldingLive.Distinct().ToArray());
        }
    }
}
