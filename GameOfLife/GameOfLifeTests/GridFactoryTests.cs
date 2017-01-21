using System.Linq;
using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class GridFactoryTests : BaseTests
    {
        private LifeState[,] _seed;
        private GridFactory _gridFactory;

        [SetUp]
        public void SetUp()
        {
            _seed = Fixture.Create<LifeState[,]>();
            _gridFactory = new GridFactory();
        }

        [Test]
        public void Create_GivenTooFewWrappingRules_ReturnsNewGrid()
        {
            var wrappingRules = Fixture.CreateMany<bool>().Take(1).ToArray();
            var grid = _gridFactory.Create(_seed, wrappingRules);

            Assert.That(grid, Is.TypeOf(typeof(Grid)));
        }

        [Test]
        public void Create_GivenTooManyWrappingRules_ReturnsNewGrid()
        {
            var wrappingRules = Fixture.CreateMany<bool>().Take(3).ToArray();
            var grid = _gridFactory.Create(_seed, wrappingRules);

            Assert.That(grid, Is.TypeOf(typeof(Grid)));
        }

        [Test]
        public void Create_GivenRightNumberOfWrappingRules_ReturnsNewGrid()
        {
            var wrappingRules = Fixture.CreateMany<bool>().Take(2).ToArray();
            var grid = _gridFactory.Create(_seed, wrappingRules);

            Assert.That(grid, Is.TypeOf(typeof(Grid)));
        }
    }
}
