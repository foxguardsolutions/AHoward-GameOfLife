using System.IO;

namespace GameOfLife
{
    public interface IConsoleReaderWriter : IConsoleWriter
    {
        TextReader In { get; set; }
        string ReadLine();
    }
}
