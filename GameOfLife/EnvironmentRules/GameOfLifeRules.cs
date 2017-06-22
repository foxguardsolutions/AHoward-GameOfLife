using System.Collections.Generic;
using System.Linq;
using GameOfLife.Environments;
using GameOfLife.Operations;

namespace GameOfLife.EnvironmentRules
{
    public class GameOfLifeRules : IEnvironmentRules
    {
        internal const int MaximumNeighboursToLive = 3;
        internal const int MinimumNeighboursToLive = 2;

        public CellOperation GetOperationToPerformOnCell(CellEnvironment environment)
        {
            return environment.Cell.Alive
                ? GetOperationToPerformOnLiveCell(environment.Neighbours)
                : GetOperationToPerformOnDeadCell(environment.Neighbours);
        }

        private bool CanBringToLife(IEnumerable<Cell> neighbours)
            => neighbours.Count(c => c.Alive) == MaximumNeighboursToLive;

        private CellOperation GetOperationToPerformOnDeadCell(IEnumerable<Cell> neighbours)
        {
            return CanBringToLife(neighbours)
                ? CellOperation.BringToLife
                : CellOperation.NoAction;
        }

        private CellOperation GetOperationToPerformOnLiveCell(IEnumerable<Cell> neighbours)
        {
            return IsUnderpopulated(neighbours) || IsOvercrowded(neighbours)
                ? CellOperation.Kill
                : CellOperation.NoAction;
        }

        private bool IsOvercrowded(IEnumerable<Cell> neighbours)
            => neighbours.Count(c => c.Alive) > MaximumNeighboursToLive;

        private bool IsUnderpopulated(IEnumerable<Cell> neighbours)
            => neighbours.Count(c => c.Alive) < MinimumNeighboursToLive;
    }
}
