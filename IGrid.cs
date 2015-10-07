namespace GameOfLife
{
    public interface IGrid
    {
        int Height { get; }
        int Width { get; }
        void NextGeneration();
        string ToString();
    }
}
