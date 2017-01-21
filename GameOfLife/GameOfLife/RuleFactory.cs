using System.Linq;

namespace GameOfLife
{
    public class RuleFactory
    {
        public IRule Create(params uint[] neighborCountsYieldingLive)
        {
            return new Rule(neighborCountsYieldingLive.Distinct().ToArray());
        }
    }
}
