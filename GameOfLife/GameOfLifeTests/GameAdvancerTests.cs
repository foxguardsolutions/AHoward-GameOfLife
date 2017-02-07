using GameOfLife;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public abstract class GameAdvancerTests : BaseTests
    {
        protected GameAdvancer Advancer { get; private set; }

        protected Mock<IRuleset> MockRuleset { get; private set; }

        [SetUp]
        public void SetUp()
        {
            SetUpMockConsole();
            Advancer = Fixture.Create<GameAdvancer>();

            MockRuleset = Fixture.Freeze<Mock<IRuleset>>();
        }

        protected abstract void SetUpMockConsole();

        protected void GivenRulesetWithCompletionStatus(bool rulesetIsComplete)
        {
            MockRuleset.Setup(r => r.IsComplete()).Returns(rulesetIsComplete);
        }
    }
}
