namespace GameOfLife
{
    public class CartesianGridCell : IGridCell
    {
        public int X { get; }
        public int Y { get; }
        public bool IsAlive { get; set; }

        public CartesianGridCell(int x, int y, bool isAlive = false)
        {
            X = x;
            Y = y;
            IsAlive = isAlive;
        }
    }
}
