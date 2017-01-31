namespace GameOfLife
{
    public interface IConsole
    {
        string ReadLine();
        void Write(string message, params object[] args);
        void WriteLine(string message, params object[] args);
    }
}
