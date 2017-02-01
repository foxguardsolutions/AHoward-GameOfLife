using System.IO;

namespace GameOfLife
{
    public interface IConsoleWriter
    {
        TextWriter Out { get; set; }
        void Write(string message, params object[] args);
        void WriteLine(string message, params object[] args);
    }
}
