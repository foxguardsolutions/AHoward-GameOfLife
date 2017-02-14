using GameOfLife;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class SettingsTests : BaseTests
    {
        private Mock<IGridFactory> _mockGridFactory;

        [SetUp]
        public void SetUp()
        {
            _mockGridFactory = new Mock<IGridFactory>();
        }

        [Test]
        public void SquareTileGridSettingsGetDefaultGrid_CreatesSquareTileGrid()
        {
            var settings = Fixture.Create<SquareTileGridSettings>();

            settings.GetDefaultGrid(_mockGridFactory.Object);

            _mockGridFactory.Verify(g => g.CreateSquareTileGrid(It.IsAny<LifeState[,]>(), false, true));
        }

        [Test]
        public void SquareTileGridB2S34SettingsGetDefaultGrid_CreatesHexTileGrid()
        {
            var settings = Fixture.Create<HexTileGridB2S34Settings>();

            settings.GetDefaultGrid(_mockGridFactory.Object);

            _mockGridFactory.Verify(g => g.CreateHexTileGrid(It.IsAny<LifeState[,]>(), true, true));
        }

        [Test]
        public void SquareTileGridB2S35SettingsGetDefaultGrid_CreatesHexTileGrid()
        {
            var settings = Fixture.Create<HexTileGridB2S35Settings>();

            settings.GetDefaultGrid(_mockGridFactory.Object);

            _mockGridFactory.Verify(g => g.CreateHexTileGrid(It.IsAny<LifeState[,]>(), true, true));
        }
    }
}
