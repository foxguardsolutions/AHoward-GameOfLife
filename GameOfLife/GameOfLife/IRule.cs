namespace GameOfLife
{
    public interface IRule
    {
        LifeState Apply(uint number);
    }
}
