using System;
using GameOfLife;

namespace GameOfLifeConsole
{
    public class ConsoleInterface
    {
        private IConsoleWriter _consoleWriter;
        private IGame _game;
        private TextCommandParser _parser;
        private CommandRunner _runner;

        private const string _instructions = "Enter a command. {0}\"d\" to display the current cell arrangement,{0}\"s\" to step,{0}\"r\" to reload the grid, or{0}\"q\" to quit{0}> ";

        public ConsoleInterface(IConsoleWriter consoleWriter, IGame game, TextCommandParser parser, CommandRunner runner)
        {
            _consoleWriter = consoleWriter;
            _game = game;
            _parser = parser;
            _runner = runner;
        }

        public void Start()
        {
            WelcomeUser();
            InitializeGame();
            RunUserCommands();
        }

        private void WelcomeUser()
        {
            _consoleWriter.WriteLine("Welcome to the game of life!");
            _consoleWriter.WriteLine("Press any key to start the game...");
        }

        private void InitializeGame()
        {
            _runner.Execute(Command.Reload, _game);
        }

        private void RunUserCommands()
        {
            var command = _parser.ParseCommand();
            do
            {
                _runner.Execute(command, _game);
                WriteStateToConsole();
                DisplayInstructions();
                command = _parser.ParseCommand(_consoleWriter.ReadLine());
            } while (command != Command.Quit);
        }

        private void WriteStateToConsole()
        {
            _consoleWriter.WriteLine("Current game state: ");
            _game.WriteCurrentPatternToConsole();
        }

        private void DisplayInstructions()
        {
            _consoleWriter.Write(string.Format(_instructions, Environment.NewLine));
        }
    }
}