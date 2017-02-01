using System.Collections.Generic;
using System.IO;
using GameOfLife;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using static GameOfLife.EnumExtension;

namespace GameOfLifeTests
{
    [TestFixture]
    public abstract class GameTests : BaseTests
    {
        protected Game Game { get; private set; }
        protected LifeState[,] Seed { get; private set; }
        protected List<LifeState> RulesAlreadySet { get; private set; }

        protected Mock<IRuleFactory> MockRuleFactory { get; private set; }
        protected Mock<IGridFactory> MockGridFactory { get; private set; }
        protected Mock<TextWriter> MockConsoleOut { get; private set; }
        protected Mock<IConsoleReaderWriter> MockConsole { get; private set; }
        private Mock<IRuleset> _mockRuleset;

        [SetUp]
        public void SetUp()
        {
            var ruleFactory = SetUpMockIRuleFactory();
            var gridFactory = SetUpMockIGridFactory();
            var textWriter = SetUpMockTextWriter();
            var console = SetUpMockIConsole(textWriter);
            var ruleset = SetUpMockIRuleset();

            Game = new Game(ruleFactory, gridFactory, console, ruleset);
            Seed = Fixture.Create<LifeState[,]>();
        }

        private IRuleFactory SetUpMockIRuleFactory()
        {
            MockRuleFactory = new Mock<IRuleFactory>();

            MockRuleFactory.Setup(r => r.Create(It.IsAny<uint[]>()))
                .Returns(new Rule(new uint[0]));

            return MockRuleFactory.Object;
        }

        private IGridFactory SetUpMockIGridFactory()
        {
            MockGridFactory = new Mock<IGridFactory>();

            MockGridFactory.Setup(
                g => g.CreateSquareTileGrid(It.IsAny<LifeState[,]>(), It.IsAny<bool>(), It.IsAny<bool>()))
               .Returns(MockGrid.GridWithGlider);

            return MockGridFactory.Object;
        }

        private IConsoleReaderWriter SetUpMockIConsole(TextWriter textWriter)
        {
            MockConsole = new Mock<IConsoleReaderWriter>();
            MockConsole.SetupGet(c => c.Out).Returns(textWriter);
            return MockConsole.Object;
        }

        private TextWriter SetUpMockTextWriter()
        {
            MockConsoleOut = new Mock<TextWriter>();
            return MockConsoleOut.Object;
        }

        private IRuleset SetUpMockIRuleset()
        {
            _mockRuleset = new Mock<IRuleset>();

            RulesAlreadySet = new List<LifeState>();
            SetUpMockIRulesetSetter(_mockRuleset, LifeState.Dead);
            SetUpMockIRulesetSetter(_mockRuleset, LifeState.Alive);
            _mockRuleset.Setup(r => r.IsComplete()).Returns(() => ForEvery<LifeState>(RulesAlreadySet.Contains));
            _mockRuleset.Setup(
                r => r.SetNextState(It.IsAny<Cell>(), It.IsAny<IEnumerable<Cell>>()))
                .Callback<Cell, IEnumerable<Cell>>(
                    (c, n) => c.SetNextState(GetDefaultRuleFor(c.CurrentState).Apply(n)));

            return _mockRuleset.Object;
        }

        private void SetUpMockIRulesetSetter(Mock<IRuleset> mock, LifeState state)
        {
            mock.SetupSet(r => { r[state] = It.IsAny<IRule>(); })
                .Callback<LifeState, IRule>((l, r) => RulesAlreadySet.Add(state));
        }

        private IRule GetDefaultRuleFor(LifeState state)
        {
            if (state == LifeState.Alive)
                return new Rule(new uint[] { 2, 3 });
            return new Rule(new uint[] { 3 });
        }
    }
}
