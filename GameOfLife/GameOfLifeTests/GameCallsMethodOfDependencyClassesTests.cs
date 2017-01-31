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
            Game.Load(Seed);

            Game.Step();

            MockConsole.Verify(c => c.WriteLine(expectedMessage));
        }

        [Test]
        public void WriteCurrentPattern_WritesCorrectNumberOfTimesToTextWriter()
        {
            Game.Load(Seed);
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
            Game.Load(Seed);

            Game.WriteCurrentPatternToConsole();

            MockConsoleOut.Verify(c => c.Write(It.IsAny<string>()));
        }

        [Test]
        public void Load_GivenWrappingRules_CallsGridFactorysCreateWithWrappingRules()
        {
            var wrapsOnRows = Fixture.Create<bool>();
            var wrapsOnColumns = Fixture.Create<bool>();

            Game.Load(Seed, wrapsOnRows, wrapsOnColumns);

            MockGridFactory.Verify(g => g.CreateSquareTileGrid(Seed, wrapsOnRows, wrapsOnColumns));
        }

        [Test]
        public void Load_WithoutWrappingRules_CallsGridFactorysCreateWithDefaultWrappingRules()
        {
            Game.Load(Seed);

            MockGridFactory.Verify(g => g.CreateSquareTileGrid(Seed, false, false));
        }

        [Test]
        public void SetRule_CallsRuleFactorysCreateAndRulesetIndexerSetter()
        {
            var state = Fixture.Create<LifeState>();
            var numbers = Fixture.Create<uint[]>();

            Game.SetRuleFor(state, numbers);

            MockRuleFactory.Verify(r => r.Create(numbers));
            Assert.That(RulesAlreadySet, Contains.Item(state));
        }
    }
}
