namespace GameOfLife
{
    public class CartesianGridCellLogic : IGridCellLogic<CartesianGridCell>
    {
        public bool DetermineIfCellLives(CartesianGridCell cell, int liveNeighbors)
        {
            return cell.IsAlive ? DoesLiveCellLive(liveNeighbors) : DoesDeadCellComeAlive(liveNeighbors);
        }

        public bool DoesLiveCellLive(int liveNeighbors)
        {
            return liveNeighbors >= 2 && liveNeighbors <= 3;
        }

        public bool DoesDeadCellComeAlive(int liveNeighbors)
        {
            return liveNeighbors == 3;
        }
    }
}
