using System;
using System.IO;

namespace GameOfLifeConsole
{
    public class ConsoleReaderWriter : IConsoleReaderWriter
    {
        private TextReader _in;
        private TextWriter _out;

        public ConsoleReaderWriter()
        {
            _in = Console.In;
            _out = Console.Out;
        }

        public string ReadLine()
        {
            return _in.ReadLine();
        }

        public void Write(string message, params object[] args)
        {
            _out.Write(message, args);
        }

        public void WriteLine(string message, params object[] args)
        {
            _out.WriteLine(message, args);
        }
    }
}
