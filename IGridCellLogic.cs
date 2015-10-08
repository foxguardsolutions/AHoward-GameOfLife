namespace GameOfLife
{
    public interface IGridCellLogic<in T>
    {
        bool DetermineIfCellLives(T cell, int liveNeighbors);
        bool DoesLiveCellLive(int liveNeighbors);
        bool DoesDeadCellComeAlive(int liveNeighbors);
    }
}
