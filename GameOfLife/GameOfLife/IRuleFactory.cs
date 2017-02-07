namespace GameOfLife
{
    public interface IRuleFactory
    {
        IRule CreateRule(params uint[] neighborCountsYieldingLive);
    }
}
