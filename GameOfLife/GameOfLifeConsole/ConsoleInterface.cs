using System;
using GameOfLife;

namespace GameOfLifeConsole
{
    public class ConsoleInterface
    {
        private const string WELCOME = "Welcome to the game of life!";
        private const string START_GAME = "Press any key to start the game...";
        private const string GAME_STATE_HEADER = "Current game state: ";
        private const string COMMAND_INSTRUCTIONS = "Enter a command. {0}\"d\" to display the current cell arrangement,{0}\"s\" to step,{0}\"r\" to reload the grid, or{0}\"q\" to quit{0}> ";

        private IConsole _console;
        private IGame _game;
        private TextCommandParser _parser;
        private CommandRunner _runner;

        public ConsoleInterface(IConsole console, IGame game, TextCommandParser parser, CommandRunner runner)
        {
            _console = console;
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
            _console.WriteLine(WELCOME);
            _console.WriteLine(START_GAME);
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
                command = _parser.ParseCommand(_console.ReadLine());
            } while (command != Command.Quit);
        }

        private void WriteStateToConsole()
        {
            _console.WriteLine(GAME_STATE_HEADER);
            _game.WriteCurrentPatternToConsole();
        }

        private void DisplayInstructions()
        {
            _console.Write(string.Format(COMMAND_INSTRUCTIONS, Environment.NewLine));
        }
    }
}