using GameOfLife;
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

            MockConsoleWriter.Verify(c => c.WriteLine(expectedMessage));
        }

        [Test]
        public void Step_GivenIncompleteRuleset_WritesMessageToConsole()
        {
            var expectedMessage = "Cannot step until rules have been defined for all possible cell states.";
            Game.Load(Seed);

            Game.Step();

            MockConsoleWriter.Verify(c => c.WriteLine(expectedMessage));
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
