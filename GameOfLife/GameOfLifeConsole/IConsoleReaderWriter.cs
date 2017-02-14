using GameOfLife;

namespace GameOfLifeConsole
{
    public interface IConsoleReaderWriter : IConsoleWriter
    {
        string ReadLine();
    }
}
