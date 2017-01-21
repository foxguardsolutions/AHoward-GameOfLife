namespace GameOfLife
{
    public class Grid
    {
        private LifeState[,] _cells;
        private bool[] _wraps;

        internal Grid(LifeState[,] seed, bool[] wrappingRules)
        {
            _wraps = wrappingRules;
            _cells = seed;
        }
    }
}
