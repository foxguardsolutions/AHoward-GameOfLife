using GameOfLife.Console.ArgumentParser;
using GameOfLife.Console.InputReader;
using GameOfLife.Console.OutputWriter;
using GameOfLife.EnvironmentRules;

namespace GameOfLife.Console
{
    public class GameOfLife
    {
        private static IInputReader _inputReader;
        private static IOutputWriter _outputWriter;

        public static void Main(string[] args)
        {
            _inputReader = InputReaderFactory.GetConsoleReader();
            _outputWriter = OutputWriterFactory.GetConsoleWriter();
            var parser = ArgumentParserFactory.CreateCommandLineArgumentParser(args);
            var filePath = parser.GetFilePathArgument();
            var wrap = parser.GetWrapArgument();
            var fileReader = InputReaderFactory.CreateFileReader(filePath);

            if (fileReader != null)
            {
                var rules = new GameOfLifeRules();
                var loader = new GameLoader(fileReader);
                var game = loader.LoadGame(rules, wrap);
                Play(game);
            }
            else
            {
                _outputWriter.WriteOutput($"File: \"{filePath}\" not found, press \"Enter\" to exit.");
                _inputReader.ReadInput();
            }
        }

        private static bool Continue(string text) => !text.ToUpper().StartsWith("Q");

        private static void DisplayGame(Game game)
        {
            _outputWriter.WriteOutput("\n");
            _outputWriter.WriteOutput(game);
            _outputWriter.WriteOutput("\n\n");
        }

        private static void Play(Game game)
        {
            var input = "";

            while (Continue(input))
            {
                DisplayGame(game);
                _outputWriter.WriteOutput("Press \"Enter\" to continue or enter \"Q/q\" to quit:");
                input = _inputReader.ReadInput();
                if (Continue(input))
                    game.Proceed();
            }
        }
    }
}
