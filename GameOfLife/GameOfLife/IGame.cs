using System.Collections.Generic;

namespace GameOfLife
{
    public interface IGame
    {
        IGrid Grid { get; set; }
        IRuleset Rules { get; set; }
        IList<IEnumerable<IEnumerable<LifeState>>> Generations { get; set; }
        void Step();
        void WriteCurrentPatternToConsole();
        IEnumerable<IEnumerable<LifeState>> GetCurrentPattern();
    }
}
