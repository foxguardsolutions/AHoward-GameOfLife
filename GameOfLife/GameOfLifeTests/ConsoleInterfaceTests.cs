using System;
using GameOfLife;
using GameOfLifeConsole;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class ConsoleInterfaceTests : BaseTests
    {
        private Mock<CommandRunner> _commandRunnerMock;
        private Mock<TextCommandParser> _commandParserMock;
        private Mock<IConsoleWriter> _consoleWriterMock;
        private Mock<IGame> _gameMock;
        private IGame _game;

        private ConsoleInterface _consoleInterface;

        [SetUp]
        public void Setup()
        {
            _commandRunnerMock = new Mock<CommandRunner>();
            _commandParserMock = new Mock<TextCommandParser>();
            _consoleWriterMock = new Mock<IConsoleWriter>();
            _gameMock = new Mock<IGame>();
            _game = _gameMock.Object;

            _consoleInterface = new ConsoleInterface(
                _consoleWriterMock.Object, _gameMock.Object, _commandParserMock.Object, _commandRunnerMock.Object);
        }

        [Test]
        public void Start_RunsThroughGameProcedure()
        {
            _consoleWriterMock.Setup(c => c.ReadLine()).Returns(Fixture.Create<string>());
            _commandParserMock.Setup(p => p.ParseCommand(It.IsAny<string>())).Returns(Command.Quit);
            _consoleInterface.Start();

            VerifyDisplaysWelcomeMessage();
            VerifyLoadsNewGame();
            VerifyDisplaysGrid();
            VerifyPromptsUserForCommand();
            VerifyParsesUserCommand();
        }

        private void VerifyDisplaysWelcomeMessage()
        {
            _consoleWriterMock.Verify(c => c.WriteLine("Welcome to the game of life!"));
        }

        private void VerifyLoadsNewGame()
        {
            _consoleWriterMock.Verify(c => c.WriteLine("Press any key to start the game..."));
            _commandRunnerMock.Verify(r => r.Execute(Command.Reload, _game));
        }

        private void VerifyDisplaysGrid()
        {
            _commandRunnerMock.Verify(r => r.Execute(Command.Display, _game));
            _consoleWriterMock.Verify(c => c.WriteLine("Current game state: "));
            _gameMock.Verify(g => g.WriteCurrentPatternToConsole());
        }

        private void VerifyPromptsUserForCommand()
        {
            _consoleWriterMock.Verify(c => c.Write(string.Format(
                "Enter a command. {0}\"d\" to display the current cell arrangement,{0}\"s\" to step,{0}\"r\" to reload the grid, or{0}\"q\" to quit{0}> ",
                Environment.NewLine)));
        }

        private void VerifyParsesUserCommand()
        {
            _commandParserMock.Verify(p => p.ParseCommand(It.IsAny<string>()));
        }
    }
}
