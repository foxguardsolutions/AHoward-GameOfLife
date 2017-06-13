namespace GameOfLife.Console.OutputWriter
{
    public static class OutputWriterFactory
    {
        private static IOutputWriter _consoleWriter;

        public static IOutputWriter GetConsoleWriter()
            => _consoleWriter ?? (_consoleWriter = new ConsoleWriter());
    }
}
