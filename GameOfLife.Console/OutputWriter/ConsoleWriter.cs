namespace GameOfLife.Console.OutputWriter
{
    public class ConsoleWriter : IOutputWriter
    {
        public void WriteOutput(object obj) => System.Console.Write(obj);
    }
}
