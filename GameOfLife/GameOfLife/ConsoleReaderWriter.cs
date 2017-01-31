using System;

namespace GameOfLife
{
    public class ConsoleReaderWriter : IConsole
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
