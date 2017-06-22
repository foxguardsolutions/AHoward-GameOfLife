using GameOfLife.Console.ArgumentParser;
using NUnit.Framework;
using Ploeh.AutoFixture;
using System.Linq;

namespace GameOfLife.Tests.Console.ArgumentParser
{
    public class CommandLineArgumentParserTests : Tests
    {
        private string[] _arguments;
        private CommandLineArgumentParser _parser;

        [SetUp]
        public new void SetUp()
        {
            _arguments = Fixture.CreateMany<string>().ToArray();
            _parser = new CommandLineArgumentParser(_arguments);
        }

        [Test]
        public void GetFilePathAgument_GivenCommandLineArguments_ReturnsFirstArgument()
        {
            var expected = _arguments[0];

            var actual = _parser.GetFilePathArgument();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetFilePathArgument_GivenEmptyArray_ReturnsBlankString()
        {
            GivenEmptyArray();

            var actual = _parser.GetFilePathArgument();

            Assert.That(actual, Is.EqualTo(""));
        }

        [Test]
        public void GetWrapArgument_GivenArrayOfOneArgument_ReturnsFalse()
        {
            GivenArrayOfOneArgument();

            var actual = _parser.GetWrapArgument();

            Assert.That(actual, Is.False);
        }

        [Test]
        public void GetWrapArgument_GivenBooleanStringForSecondArgument_ReturnsBooleanValueOfSecondArgument()
        {
            var expected = Fixture.Create<bool>();
            _arguments[1] = expected.ToString();

            var actual = _parser.GetWrapArgument();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetWrapArgument_GivenNonBooleanStringForSecondArgument_ReturnsFalse()
        {
            var actual = _parser.GetWrapArgument();

            Assert.That(actual, Is.False);
        }

        public void GivenArrayOfOneArgument()
        {
            _arguments = new string[1] { Fixture.Create<string>() };
            _parser = new CommandLineArgumentParser(_arguments);
        }

        public void GivenEmptyArray()
        {
            _arguments = new string[0];
            _parser = new CommandLineArgumentParser(_arguments);
        }
    }
}
