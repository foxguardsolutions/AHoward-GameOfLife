using GameOfLife;
using Moq;
using NUnit.Framework;

namespace GameOfLifeTests
{
    public abstract class GameConfiguratorTests : BaseTests
    {
        protected Mock<IGridFactory> MockGridFactory { get; private set; }
        protected Mock<IRuleFactory> MockRuleFactory { get; private set; }
        protected Mock<IGame> MockGame { get; private set; }

        protected GameConfigurator Configurator { get; private set; }

        [SetUp]
        public void SetupConfigurator()
        {
            MockGridFactory = new Mock<IGridFactory>();
            MockRuleFactory = new Mock<IRuleFactory>();
            MockGame = new Mock<IGame>();

            Configurator = new GameConfigurator(MockGridFactory.Object, MockRuleFactory.Object, MockGame.Object);
        }
    }
}
