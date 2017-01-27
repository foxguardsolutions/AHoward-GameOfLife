namespace GameOfLife
{
    public interface IConsoleWriter
    {
        string ReadLine();
        void Write(string message, params object[] args);
        void WriteLine(string message, params object[] args);
    }
}
