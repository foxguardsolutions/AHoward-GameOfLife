using System.IO;

namespace GameOfLife
{
    public interface IConsole
    {
        TextReader In { get; set; }
        TextWriter Out { get; set; }
        string ReadLine();
        void Write(string message, params object[] args);
        void WriteLine(string message, params object[] args);
    }
}
