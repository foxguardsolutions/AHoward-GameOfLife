using System.IO;

namespace GameOfLife.Console.InputReader
{
    public class InputReaderFactory
    {
        private static IInputReader _consoleReader;

        public static IInputReader CreateFileReader(string filePath)
            => File.Exists(filePath) ? new FileReader(filePath) : null;

        public static IInputReader GetConsoleReader()
            => _consoleReader ?? (_consoleReader = new ConsoleReader());
    }
}
