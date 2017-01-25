using GameOfLife;
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
            _seed = Fixture.Create<LifeState[,]>();
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
        public void Create_ReturnsGridWithCellsThatHaveInitialStateMatchingSeedValues()
        {
            var grid = _gridFactory.CreateSquareTileGrid(_seed, _wrapsOnRows, _wrapsOnColumns);

            AssertGridCellsHaveInitialStateMatchingSeedValues(grid, _seed);
        }

        private void AssertGridCellsHaveInitialStateMatchingSeedValues(SquareTileGrid grid, LifeState[,] seed)
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
