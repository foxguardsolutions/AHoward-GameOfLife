using GameOfLife.Environments;
using NUnit.Framework;
using System.Linq;

namespace GameOfLife.Tests.Environments
{
    [TestFixture]
    public class WrappedEnvironmentTests : TestsUsingEnvironments
    {
        private const int NumberOfNeighbours = 8;

        private WrappedEnvironment _environment;

        [TestCaseSource(nameof(EdgeNumbers))]
        public void Neighbours_GivenEnvironmentAtCornerOfGrid_HasCountOf3(int corner)
        {
            GivenEnvironmentAtCornerOfGrid(corner);
            var expected = NumberOfNeighbours;

            Assert.That(_environment.Neighbours.Count(), Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(EdgeNumbers))]
        public void Neighbours_GivenEnvironmentAtSideOfGrid_HasCountOf5(int side)
        {
            GivenEnvironmentAtSideOfGrid(side);
            var expected = NumberOfNeighbours;

            Assert.That(_environment.Neighbours.Count(), Is.EqualTo(expected));
        }

        [Test]
        public void Neighbours_GivenEnvironmentAwayFromEdgeOfGrid_HasCountOf8()
        {
            GivenEnvironmentAwayFromEdgeOfGrid();
            var expected = NumberOfNeighbours;

            Assert.That(_environment.Neighbours.Count(), Is.EqualTo(expected));
        }

        private void GivenEnvironmentAtCornerOfGrid(int cornerNumber)
        {
            GivenCorner(cornerNumber);
            _environment = new WrappedEnvironment(Row, Column, Grid);
        }

        private void GivenEnvironmentAtSideOfGrid(int sideNumber)
        {
            GivenSide(sideNumber);
            _environment = new WrappedEnvironment(Row, Column, Grid);
        }

        private void GivenEnvironmentAwayFromEdgeOfGrid()
        {
            GivenColumnAwayFromEdgeOfGrid();
            GivenRowAwayFromEdgeOfGrid();
            _environment = new WrappedEnvironment(Row, Column, Grid);
        }
    }
}
