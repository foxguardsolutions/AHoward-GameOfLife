using GameOfLife;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class GridFactoryTests : BaseTests
    {
        private LifeState[,] _seed;
        private bool _wrapsOnRows;
        private bool _wrapsOnColumns;
        private GridFactory _gridFactory;

        [SetUp]
        public void SetUp()
        {
            _seed = Fixture.Freeze<LifeState[,]>();
            _wrapsOnRows = Fixture.Create<bool>();
            _wrapsOnColumns = Fixture.Create<bool>();
            _gridFactory = new GridFactory();
        }

        [Test]
        public void CreateSquareTileGrid_GivenNoWrappingRules_ReturnsNewSquareTileGrid()
        {
            var grid = _gridFactory.CreateSquareTileGrid(_seed);

            Assert.That(grid, Is.TypeOf(typeof(SquareTileGrid)));
        }

        [Test]
        public void CreateSquareTileGrid_GivenWrappingRules_ReturnsNewSquareTileGrid()
        {
            var grid = _gridFactory.CreateSquareTileGrid(_seed, _wrapsOnRows, _wrapsOnColumns);

            Assert.That(grid, Is.TypeOf(typeof(SquareTileGrid)));
        }

        [Test]
        public void CreateSquareTileGrid_ReturnsGridWithCellsThatHaveInitialStateMatchingSeedValues()
        {
            var grid = _gridFactory.CreateSquareTileGrid(_seed, _wrapsOnRows, _wrapsOnColumns);

            AssertGridCellsHaveInitialStateMatchingSeedValues(grid, _seed);
        }

        [Test]
        public void CreateDefaultGrid_ReturnsGridWithCellsThatHaveInitialStateMatchingDefaultSeedValues()
        {
            var mockSettings = new Mock<ISettings>();

            _gridFactory.CreateDefaultGrid(mockSettings.Object);

            mockSettings.Verify(s => s.GetDefaultGrid(_gridFactory));
        }

        [Test]
        public void CreateHexTileGrid_GivenNoWrappingRules_ReturnsNewHexTileGrid()
        {
            var grid = _gridFactory.CreateHexTileGrid(_seed);

            Assert.That(grid, Is.TypeOf(typeof(HexTileGrid)));
        }

        [Test]
        public void CreateHexTileGrid_GivenWrappingRules_ReturnsNewHexTileGrid()
        {
            var grid = _gridFactory.CreateHexTileGrid(_seed, _wrapsOnRows, _wrapsOnColumns);

            Assert.That(grid, Is.TypeOf(typeof(HexTileGrid)));
        }

        private void AssertGridCellsHaveInitialStateMatchingSeedValues(IGrid grid, LifeState[,] seed)
        {
            foreach (var position in grid)
            {
                var cell = grid.GetCellAt(position);
                var seedValue = seed[position.DimensionOne, position.DimensionTwo];
                Assert.That(cell.CurrentState, Is.EqualTo(seedValue));
            }
        }
    }
}
