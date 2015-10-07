namespace GameOfLife
{
    public class CartesianGridLogic : IGridLogic<CartesianGridCell>
    {
        private readonly CartesianGrid cartesianGrid;

        public CartesianGridLogic(CartesianGrid grid)
        {
            cartesianGrid = grid;
        }

        public int NumberOfLiveNeighbors(CartesianGridCell cell)
        {
            int liveNeighbors = 0;

            for (int x = cell.X - 1; x <= cell.X + 1; x++)
            {
                for (int y = cell.Y - 1; y <= cell.Y + 1; y++)
                {
                    if (IsNeighborAlive(cell, x, y))
                    {
                        liveNeighbors++;
                    }
                }
            }

            return liveNeighbors;
        }

        public bool IsNeighborAlive(CartesianGridCell cell, int x, int y)
        {
            return IsCellIndexValid(x, y) && !(x == cell.X && y == cell.Y) && cartesianGrid.Map[x][y].IsAlive;
        }

        public bool IsCellIndexValid(int x, int y)
        {
            return x >= 0 && x < cartesianGrid.Width && y >= 0 && y < cartesianGrid.Height;
        }
    }
}
