namespace GameOfLife
{
    public interface IGame
    {
        void Start();
        void Load(LifeState[,] seed, bool wrapsOnRows, bool wrapsOnColumns);
        void SetRuleFor(LifeState state, params uint[] neighborCountsYieldingLive);
        void Step();
        LifeState[,] GetCurrentPattern();
    }
}
