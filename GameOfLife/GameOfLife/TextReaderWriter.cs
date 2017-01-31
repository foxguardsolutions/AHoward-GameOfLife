using System;
using System.IO;

namespace GameOfLife
{
    public class TextReaderWriter : IConsole
    {
        public TextReader In { get; set; }
        public TextWriter Out { get; set; }

        public TextReaderWriter()
        {
            In = Console.In;
            Out = Console.Out;
        }

        public string ReadLine()
        {
            return In.ReadLine();
        }

        public void Write(string message, params object[] args)
        {
            Out.Write(message, args);
        }

        public void WriteLine(string message, params object[] args)
        {
            Out.WriteLine(message, args);
        }
    }
}
