namespace GameOfLife
{
    public interface IGridWriter
    {
        IConsoleWriter DefaultWriter { get; set; }

        void WriteCurrentStateOf(IGrid grid);
    }
}