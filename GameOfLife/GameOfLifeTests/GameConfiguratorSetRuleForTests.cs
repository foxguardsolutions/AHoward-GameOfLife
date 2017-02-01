using GameOfLife;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class GameConfiguratorSetRuleForTests : GameConfiguratorTests
    {
        private Mock<IRuleset> _mockRuleset;

        [SetUp]
        public void SetUp()
        {
            _mockRuleset = new Mock<IRuleset>();
            MockGame.SetupProperty(g => g.Rules, _mockRuleset.Object);

            MockRuleFactory.Setup(r => r.Create(It.IsAny<uint[]>())).Returns(new Rule(new uint[0]));
        }

        [Test]
        public void SetRuleFor_GivenRuleParameters_CallsRuleFactoryMethod()
        {
            var state = Fixture.Create<LifeState>();
            var numbersYieldingLive = Fixture.Create<uint[]>();

            Configurator.SetRuleFor(state, numbersYieldingLive);

            MockRuleFactory.Verify(r => r.Create(numbersYieldingLive));
        }

        [Test]
        public void SetRuleFor_GivenRuleParameters_CallsRulesetIndexer()
        {
            var state = Fixture.Create<LifeState>();
            var numbersYieldingLive = Fixture.Create<uint[]>();

            Configurator.SetRuleFor(state, numbersYieldingLive);

            _mockRuleset.VerifySet(r => r[state] = It.IsAny<IRule>());
        }

        [Test]
        public void LoadDefaultConfiguration_CallsRuleFactoryMethodWithBothDefaultRuleNumberSets()
        {
            Configurator.LoadDefaultConfiguration();

            MockRuleFactory.Verify(r => r.Create(DefaultSettings.SurvivalNumbers));
            MockRuleFactory.Verify(r => r.Create(DefaultSettings.ReproductionNumbers));
        }

        [Test]
        public void LoadDefaultConfiguration_CallsRulesetIndexerToSetSurvivalAndReproductionRules()
        {
            var mockRule = new Mock<IRule>();
            var survivalRule = mockRule.Object;
            var reproductionRule = mockRule.Object;
            MockRuleFactory.Setup(r => r.Create(DefaultSettings.SurvivalNumbers)).Returns(survivalRule);
            MockRuleFactory.Setup(r => r.Create(DefaultSettings.ReproductionNumbers)).Returns(reproductionRule);

            Configurator.LoadDefaultConfiguration();

            _mockRuleset.VerifySet(r => r[LifeState.Alive] = survivalRule);
            _mockRuleset.VerifySet(r => r[LifeState.Dead] = reproductionRule);
        }
    }
}
