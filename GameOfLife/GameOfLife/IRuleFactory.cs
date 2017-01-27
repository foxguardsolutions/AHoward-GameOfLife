namespace GameOfLife
{
    public interface IRuleFactory
    {
        IRule Create(params uint[] neighborCountsYieldingLive);
    }
}
