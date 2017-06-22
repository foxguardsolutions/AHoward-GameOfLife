namespace GameOfLife.Console.ArgumentParser
{
    public static class ArgumentParserFactory
    {
        public static IArgumentParser CreateCommandLineArgumentParser(string[] args)
            => new CommandLineArgumentParser(args);
    }
}
