namespace GameOfLife
{
    public interface IGameAdvancer
    {
        void Step(IGrid grid, IRuleset rules);
    }
}