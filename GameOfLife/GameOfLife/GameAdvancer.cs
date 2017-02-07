namespace GameOfLife
{
    public class GameAdvancer : IGameAdvancer
    {
        private const string GRID_NOT_YET_LOADED = "No steps taken.  A grid must be loaded before any steps can be taken.";
        private const string INCOMPLETE_RULES = "No steps taken.  Cannot step until rules have been defined for all possible cell states.";

        private IConsoleWriter _consoleWriter;

        public GameAdvancer(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }

        public void Step(IGrid grid, IRuleset rules)
        {
            if (IsReadyToStep(rules))
            {
                SetNextStateOfAllCells(grid, rules);
                AdvanceStatesOfAllCells(grid);
            }
        }

        private bool IsReadyToStep(IRuleset rules)
        {
            if (!rules.IsComplete())
            {
                _consoleWriter.WriteLine(INCOMPLETE_RULES);
                return false;
            }

            return true;
        }

        private void SetNextStateOfAllCells(IGrid grid, IRuleset rules)
        {
            foreach (var position in grid)
                SetNextStateOfCellAt(position, grid, rules);
        }

        private void SetNextStateOfCellAt(CellPosition position, IGrid grid, IRuleset rules)
        {
            var cell = grid.GetCellAt(position);
            var neighbors = grid.GetNeighborsOfCellAt(position);
            rules.SetNextStateOfCellGivenNeighbors(cell, neighbors);
        }

        private void AdvanceStatesOfAllCells(IGrid grid)
        {
            foreach (var position in grid)
                AdvanceStateOfCellAt(position, grid);
        }

        private void AdvanceStateOfCellAt(CellPosition position, IGrid grid)
        {
            var cell = grid.GetCellAt(position);
            cell.AdvanceState();
        }
    }
}
