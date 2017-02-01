using GameOfLife;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class GameConfiguratorLoadGridTests : GameConfiguratorTests
    {
        private LifeState[,] _seed;

        [SetUp]
        public void SetUp()
        {
            MockGridFactory.Setup(
                g => g.CreateSquareTileGrid(It.IsAny<LifeState[,]>(), It.IsAny<bool>(), It.IsAny<bool>()))
               .Returns(MockGrid.GridWithGlider);

            _seed = Fixture.Create<LifeState[,]>();
        }

        [Test]
        public void LoadGrid_GivenWrappingRules_CallsCreateMethodOfGridFactory()
        {
            var wrapsOnRows = Fixture.Create<bool>();
            var wrapsOnColumns = Fixture.Create<bool>();

            Configurator.LoadGrid(_seed, wrapsOnRows, wrapsOnColumns);

            MockGridFactory.Verify(
                g => g.CreateSquareTileGrid(
                    It.IsAny<LifeState[,]>(), It.IsAny<bool>(), It.IsAny<bool>()));
        }

        [Test]
        public void LoadGrid_GivenNoWrappingRules_CallsCreateMethodOfGridFactory()
        {
            Configurator.LoadGrid(_seed);

            MockGridFactory.Verify(
                g => g.CreateSquareTileGrid(
                    It.IsAny<LifeState[,]>(), false, false));
        }

        [Test]
        public void LoadGrid_GivenWrappingRules_SetsGridPropertyOfGame()
        {
            var wrapsOnRows = Fixture.Create<bool>();
            var wrapsOnColumns = Fixture.Create<bool>();

            Configurator.LoadGrid(_seed, wrapsOnRows, wrapsOnColumns);

            MockGame.VerifySet(g => g.Grid = It.IsAny<IGrid>());
        }

        [Test]
        public void LoadGrid_GivenNoWrappingRules_SetsGridPropertyOfGame()
        {
            Configurator.LoadGrid(_seed);

            MockGame.VerifySet(g => g.Grid = It.IsAny<IGrid>());
        }

        [Test]
        public void LoadDefaultConfiguration_CallsCreateMethodOfGridFactoryWithDefaultGridAndWrappingOnBothDimensions()
        {
            var mockRuleset = new Mock<IRuleset>();
            MockGame.SetupProperty(g => g.Rules, mockRuleset.Object);

            Configurator.LoadDefaultConfiguration();

            MockGridFactory.Verify(
                g => g.CreateSquareTileGrid(
                    DefaultSettings.Seed, true, true));
        }
    }
}
