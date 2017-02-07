using System;
using GameOfLife;
using GameOfLifeConsole;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class ConsoleGameTests : BaseTests
    {
        private Mock<ICommandRunner> _commandRunnerMock;
        private Mock<TextCommandParser> _commandParserMock;
        private Mock<IConsoleReaderWriter> _consoleMock;

        private GameOfLifeConsole.ConsoleGame _consoleInterface;

        [SetUp]
        public void Setup()
        {
            _commandParserMock = new Mock<TextCommandParser>();
            Fixture.Register(() => _commandParserMock.Object);
            _commandRunnerMock = Fixture.Freeze<Mock<ICommandRunner>>();
            _consoleMock = Fixture.Freeze<Mock<IConsoleReaderWriter>>();

            _consoleInterface = Fixture.Create<ConsoleGame>();
        }

        [Test]
        public void Start_RunsThroughGameProcedure()
        {
            GivenUserSuppliesQuitCommandWhenPrompted();

            _consoleInterface.Start();

            VerifyDisplaysWelcomeMessage();
            VerifyLoadsNewGame();
            VerifyDisplaysGrid();
            VerifyPromptsUserForCommand();
            VerifyParsesUserCommand();
        }

        private void GivenUserSuppliesQuitCommandWhenPrompted()
        {
            _consoleMock.Setup(c => c.ReadLine()).Returns(It.IsAny<string>());
            _commandParserMock.Setup(c => c.ParseCommand(It.IsAny<string>())).Returns(Command.Quit);
        }

        private void VerifyDisplaysWelcomeMessage()
        {
            _consoleMock.Verify(c => c.WriteLine("Welcome to the game of life!"));
        }

        private void VerifyLoadsNewGame()
        {
            _consoleMock.Verify(c => c.WriteLine("Press any key to start the game..."));
            _commandRunnerMock.Verify(r => r.Execute(Command.Reload));
        }

        private void VerifyDisplaysGrid()
        {
            _consoleMock.Verify(c => c.WriteLine("Current game state: "));
            _commandRunnerMock.Verify(r => r.Execute(Command.Display));
        }

        private void VerifyPromptsUserForCommand()
        {
            _consoleMock.Verify(c => c.Write(string.Format(
                "Enter a command. {0}\"d\" to display the current cell arrangement,{0}\"s\" to step,{0}\"r\" to reload the grid, or{0}\"q\" to quit{0}> ",
                Environment.NewLine)));
        }

        private void VerifyParsesUserCommand()
        {
            _commandParserMock.Verify(p => p.ParseCommand(It.IsAny<string>()));
        }
    }
}
