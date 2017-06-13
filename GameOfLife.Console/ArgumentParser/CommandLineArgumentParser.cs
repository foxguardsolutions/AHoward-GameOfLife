namespace GameOfLife.Console.ArgumentParser
{
    public class CommandLineArgumentParser : IArgumentParser
    {
        private readonly string[] _arguments;

        public CommandLineArgumentParser(string[] arguments)
        {
            _arguments = arguments;
        }

        public string GetFilePathArgument() => _arguments.Length > 0 ? _arguments[0] : "";

        public bool GetWrapArgument()
            => _arguments.Length > 1 && bool.TryParse(_arguments[1], out bool wrap) && wrap;

    }
}
