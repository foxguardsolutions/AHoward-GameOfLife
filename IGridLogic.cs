namespace GameOfLife
{
    public interface IGridLogic<in T>
    {
        int NumberOfLiveNeighbors(T cell);
        bool IsNeighborAlive(T cell, int x, int y);
        bool IsCellIndexValid(int x, int y);
    }
}
