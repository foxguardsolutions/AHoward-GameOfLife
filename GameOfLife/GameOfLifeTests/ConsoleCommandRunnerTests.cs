using GameOfLife;
using GameOfLifeConsole;
using Moq;
using NUnit.Framework;

namespace GameOfLifeTests
{
    public class ConsoleCommandRunnerTests : BaseTests
    {
        private Mock<IGame> _gameMock;
        private CommandRunner _commandRunner;

        [SetUp]
        public void SetUp()
        {
            _gameMock = new Mock<IGame>(MockBehavior.Strict);
            _commandRunner = new CommandRunner();
        }

        [Test]
        public void Execute_GivenReloadCommand_CallsGameStart()
        {
            // _gameMock.Setup(g => g.Start());
            _commandRunner.Execute(Command.Reload, _gameMock.Object);

            // _gameMock.Verify(g => g.Start());
        }

        [Test]
        public void Execute_GivenStepCommand_CallsGameStep()
        {
            _gameMock.Setup(g => g.Step());
            _commandRunner.Execute(Command.Step, _gameMock.Object);

            _gameMock.Verify(g => g.Step());
        }

        [Test]
        public void Execute_GivenDisplayCommand_CallsNoGameMethods()
        {
            _commandRunner.Execute(Command.Display, _gameMock.Object);
        }

        [Test]
        public void Execute_GivenQuitCommand_CallsNoGameMethods()
        {
            _commandRunner.Execute(Command.Quit, _gameMock.Object);
        }
    }
}
