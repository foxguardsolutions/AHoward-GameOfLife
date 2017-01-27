using System;

namespace GameOfLife
{
    public class ConsoleWriter : IConsoleWriter
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string message, params object[] args)
        {
            Console.Write(message, args);
        }

        public void WriteLine(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }
    }
}
