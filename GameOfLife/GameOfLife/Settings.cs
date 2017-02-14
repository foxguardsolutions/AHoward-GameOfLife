using System.Collections.Generic;

namespace GameOfLife
{
    public abstract class Settings
    {
        public Dictionary<LifeState, string> StateRepresentations
        {
            get
            {
                return new Dictionary<LifeState, string>
                    {
                        { LifeState.Alive, "*" },
                        { LifeState.Dead, "." }
                    };
            }
        }

        public LifeState[,] DefaultSeed
        {
            get { return MakeDefaultSeed(); }
        }

        protected abstract LifeState[,] MakeDefaultSeed();
    }
}
