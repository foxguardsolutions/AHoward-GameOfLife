using GameOfLife;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class GridWriterTests : BaseTests
    {
        private Mock<IConsoleWriter> _mockConsole;
        private GridWriter _gridWriter;

        [SetUp]
        public void SetUp()
        {
            _mockConsole = Fixture.Freeze<Mock<IConsoleWriter>>();
            _gridWriter = Fixture.Create<GridWriter>();
        }

        [Test]
        public void WriteCurrentStateOfAnyGrid_WritesToConsole()
        {
            var grid = Fixture.Create<SquareTileGrid>();

            _gridWriter.WriteCurrentStateOf(grid);

            _mockConsole.Verify(c => c.WriteLine(It.IsAny<string>()));
        }

        [Test]
        public void WriteCurrentStateOfKnownGrid_WritesCorrectStringToConsole()
        {
            var grid = MockObjects.GridWithGliderPattern;

            _gridWriter.WriteCurrentStateOf(grid);

            _mockConsole.Verify(c => c.WriteLine(MockObjects.RepresentationOfGridWithGliderPattern));
        }
    }
}
