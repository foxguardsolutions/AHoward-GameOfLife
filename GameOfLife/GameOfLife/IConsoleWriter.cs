namespace GameOfLife
{
    public interface IConsoleWriter
    {
        void Write(string message, params object[] args);
        void WriteLine(string message, params object[] args);
    }
}
