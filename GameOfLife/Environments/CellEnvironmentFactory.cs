using System.Collections.Generic;

namespace GameOfLife.Environments
{
    public class CellEnvironmentFactory
    {
        public CellEnvironment GetEnvironment(int row, int column, Grid grid)
        {
            return new EdgedEnvironment(row, column, grid);
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
