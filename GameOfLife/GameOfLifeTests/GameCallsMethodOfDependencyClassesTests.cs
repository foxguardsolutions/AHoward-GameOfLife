using System;
using System.Linq;
using GameOfLife;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class GameCallsMethodOfDependencyClassesTests : GameTests
    {
        [Test]
        public void Step_BeforeGridLoad_WritesMessageToConsole()
        {
            var expectedMessage = "A grid must be loaded before any steps can be taken.";

            Game.Step();

            MockConsole.Verify(c => c.WriteLine(expectedMessage));
        }

        [Test]
        public void Step_GivenIncompleteRuleset_WritesMessageToConsole()
        {
            var expectedMessage = "Cannot step until rules have been defined for all possible cell states.";

            // Game.Load(Seed);
            // ^ Mock this
            Game.Step();

            MockConsole.Verify(c => c.WriteLine(expectedMessage));
        }

        [Test]
        public void WriteCurrentPattern_WritesCorrectNumberOfTimesToTextWriter()
        {
            // Game.Load(Seed);
            // ^ Mock this
            var expectedPattern = MockGrid.GridWithGlider.GetCurrentPattern();
            var expectedNumberOfNewLines = expectedPattern.Count();
            var expectedNumberOfCellsWritten = expectedPattern.Count() * expectedPattern.First().Count();
            var expectedNumberOfTotalWrites = expectedNumberOfCellsWritten + expectedNumberOfNewLines;

            Game.WriteCurrentPattern(MockConsoleOut.Object);

            MockConsoleOut.Verify(c => c.Write(Environment.NewLine), Times.Exactly(expectedNumberOfNewLines));
            MockConsoleOut.Verify(c => c.Write(It.IsAny<string>()), Times.Exactly(expectedNumberOfTotalWrites));
        }

        [Test]
        public void WriteCurrentPatternToConsole_WritesToConsole()
        {
            // Game.Load(Seed);
            // ^ Mock this
            Game.WriteCurrentPatternToConsole();

            MockConsoleOut.Verify(c => c.Write(It.IsAny<string>()));
        }
    }
}
