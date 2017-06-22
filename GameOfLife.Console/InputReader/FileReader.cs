using System.IO;

namespace GameOfLife.Console.InputReader
{
    public class FileReader : IInputReader
    {
        private readonly string _filePath;

        public FileReader(string filePath)
        {
            _filePath = filePath;
        }

        public string ReadInput() => string.Join(GridFactory.NewLine, File.ReadAllLines(_filePath));
    }
}
