using System.Collections.Generic;

namespace GameOfLife.Environments
{
    public class CellEnvironmentFactory
    {
        private readonly bool _wrap;

        public CellEnvironmentFactory(bool wrap)
        {
            _wrap = wrap;
        }

        public CellEnvironment GetEnvironment(int row, int column, Grid grid)
        {
            CellEnvironment environment;

            if (_wrap)
                environment = new WrappedEnvironment(row, column, grid);
            else
                environment = new EdgedEnvironment(row, column, grid);

            return environment;
        }

        public IEnumerable<CellEnvironment> GetEnvironments(Grid grid)
        {
            var environments = new List<CellEnvironment>();

            for (var row = 0; row < grid.Height; row++)
                for (var column = 0; column < grid.Width; column++)
                    environments.Add(GetEnvironment(row, column, grid));

            return environments;
        }
    }
}
