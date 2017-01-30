using System.Collections.Generic;

namespace GameOfLife
{
    public static class DefaultSettings
    {
        public static LifeState[,] Seed
        {
            get { return MakeDefaultSeed(); }
        }

        private static LifeState[,] MakeDefaultSeed()
        {
            var seed = new LifeState[5, 10];
            seed[0, 4] = LifeState.Alive;
            seed[1, 2] = LifeState.Alive;
            seed[1, 4] = LifeState.Alive;
            seed[2, 3] = LifeState.Alive;
            seed[2, 4] = LifeState.Alive;
            return seed;
        }

        private static Dictionary<LifeState, string> _stateRepresentations = new Dictionary<LifeState, string>
        {
            { LifeState.Alive, "*" },
            { LifeState.Dead, "-" }
        };

        public static string ToString(LifeState state)
        {
            return _stateRepresentations[state];
        }
    }
}
