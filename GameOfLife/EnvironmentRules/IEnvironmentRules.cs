using GameOfLife.Environments;
using GameOfLife.Operations;

namespace GameOfLife.EnvironmentRules
{
    public interface IEnvironmentRules
    {
        CellOperation GetOperationToPerformOnCell(CellEnvironment environment);
    }
}
