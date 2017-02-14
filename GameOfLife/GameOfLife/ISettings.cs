using System.Collections.Generic;

namespace GameOfLife
{
    public interface ISettings
    {
        Dictionary<LifeState, string> StateRepresentations { get; }
        IGrid GetDefaultGrid(IGridFactory gridFactory);
        uint[] SurvivalNumbers { get; }
        uint[] ReproductionNumbers { get; }
        LifeState[,] DefaultSeed { get; }
    }
}
