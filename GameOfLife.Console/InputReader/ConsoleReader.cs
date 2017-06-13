namespace GameOfLife.Console.InputReader
{
    public class ConsoleReader : IInputReader
    {
        public string ReadInput() => System.Console.ReadLine();
    }
}
