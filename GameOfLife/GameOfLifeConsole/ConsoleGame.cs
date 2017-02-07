using System;
using GameOfLife;

namespace GameOfLifeConsole
{
    public class ConsoleGame
    {
        private const string WELCOME = "Welcome to the game of life!";
        private const string START_GAME = "Press any key to start the game...";
        private const string GAME_STATE_HEADER = "Current game state: ";
        private const string COMMAND_INSTRUCTIONS =
            "Enter a command. {0}" +
            "\"d\" to display the current cell arrangement,{0}" +
            "\"s\" to step,{0}" +
            "\"r\" to reload the grid, or{0}" +
            "\"q\" to quit{0}> ";

        private IConsoleReaderWriter _console;
        private TextCommandParser _parser;
        private ICommandRunner _runner;

        public ConsoleGame(IConsoleReaderWriter console, TextCommandParser parser, ICommandRunner runner)
        {
            _console = console;
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
            _runner.Execute(Command.Reload);
        }

        private void RunUserCommands()
        {
            var command = _parser.ParseCommand();
            do
            {
                ExecuteSuppliedCommand(command);
                command = GetNextCommandFromClient();
            } while (command != Command.Quit);
        }

        private void ExecuteSuppliedCommand(Command command)
        {
            if (command != Command.Display)
                _runner.Execute(command);
            DisplayCurrentGridState();
        }

        private void DisplayCurrentGridState()
        {
            _console.WriteLine(GAME_STATE_HEADER);
            _runner.Execute(Command.Display);
        }

        private Command GetNextCommandFromClient()
        {
            PromptUserForNextCommand();
            var clientInput = _console.ReadLine();
            return _parser.ParseCommand(clientInput);
        }

        private void PromptUserForNextCommand()
        {
            _console.Write(string.Format(COMMAND_INSTRUCTIONS, Environment.NewLine));
        }
    }
}