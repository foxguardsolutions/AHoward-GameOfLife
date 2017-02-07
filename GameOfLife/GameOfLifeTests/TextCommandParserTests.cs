using GameOfLife;
using GameOfLifeConsole;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class TextCommandParserTests : BaseTests
    {
        private TextCommandParser _commandParser;

        [SetUp]
        public void SetUp()
        {
            _commandParser = new TextCommandParser();
        }

        [Test]
        public void Parse_WithoutArguments_ReturnsDisplayCommand()
        {
            var command = _commandParser.ParseCommand();

            Assert.That(command, Is.EqualTo(Command.Display));
        }

        [Test]
        public void Parse_WithInputStartingWithD_ReturnsDisplayCommand()
        {
            var input = Fixture.Create("D");
            var command = _commandParser.ParseCommand(input);

            Assert.That(command, Is.EqualTo(Command.Display));
        }

        [Test]
        public void Parse_WithInputStartingWithR_ReturnsReloadCommand()
        {
            var input = Fixture.Create("R");
            var command = _commandParser.ParseCommand(input);

            Assert.That(command, Is.EqualTo(Command.Reload));
        }

        [Test]
        public void Parse_WithInputStartingWithQ_ReturnsQuitCommand()
        {
            var input = Fixture.Create("Q");
            var command = _commandParser.ParseCommand(input);

            Assert.That(command, Is.EqualTo(Command.Quit));
        }

        [Test]
        public void Parse_WithInputStartingWithS_ReturnsStepCommand()
        {
            var input = Fixture.Create("S");
            var command = _commandParser.ParseCommand(input);

            Assert.That(command, Is.EqualTo(Command.Step));
        }
    }
}
