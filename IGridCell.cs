namespace GameOfLife
{
    public interface IGridCell
    {
        int X { get; }
        int Y { get; }
        bool IsAlive { get; set; }
    }
}
