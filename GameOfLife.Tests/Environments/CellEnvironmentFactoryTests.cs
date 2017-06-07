using GameOfLife.Environments;
using NUnit.Framework;
using Ploeh.AutoFixture;
using System.Linq;

namespace GameOfLife.Tests.Environments
{
    [TestFixture]
    public class CellEnvironmentFactoryTests : TestsUsingEnvironments
    {
        private CellEnvironmentFactory _factory;

        [SetUp]
        public new void SetUp()
        {
            _factory = Fixture.Create<CellEnvironmentFactory>();
        }

        [Test]
        public void GetEnvironment_ReturnsEdgedEnvironment()
        {
            var actual = _factory.GetEnvironment(Row, Column, Grid);

            Assert.That(actual, Is.TypeOf<EdgedEnvironment>());
        }

        [Test]
        public void GetEnvironments_GivenGrid_ReturnsEnvironmentForEachCellOnGrid()
        {
            var expectedCount = Grid.Height * Grid.Width;

            var actual = _factory.GetEnvironments(Grid);

            Assert.That(actual.Count(), Is.EqualTo(expectedCount));
        }
    }
}
