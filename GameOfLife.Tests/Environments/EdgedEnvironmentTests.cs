using GameOfLife.Environments;
using NUnit.Framework;
using System.Linq;

namespace GameOfLife.Tests.Environments
{
    [TestFixture]
    class EdgedEnvironmentTests : TestsUsingEnvironments
    {
        private const int NumberOfNeighboursAtCorner = 3;
        private const int NumberOfNeighboursAtSide = 5;
        private const int NumberOfNeighboursAwayFromEdge = 8;

        private EdgedEnvironment _environment;

        [TestCaseSource(nameof(EdgeNumbers))]
        public void Neighbours_GivenEnvironmentAtCornerOfGrid_HasCountOf3(int corner)
        {
            GivenEnvironmentAtCornerOfGrid(corner);
            var expected = NumberOfNeighboursAtCorner;
            
            Assert.That(_environment.Neighbours.Count(), Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(EdgeNumbers))]
        public void Neighbours_GivenEnvironmentAtSideOfGrid_HasCountOf5(int side)
        {
            GivenEnvironmentAtSideOfGrid(side);
            var expected = NumberOfNeighboursAtSide;

            Assert.That(_environment.Neighbours.Count(), Is.EqualTo(expected));
        }

        [Test]
        public void Neighbours_GivenEnvironmentAwayFromEdgeOfGrid_HasCountOf8()
        {
            GivenEnvironmentAwayFromEdgeOfGrid();
            var expected = NumberOfNeighboursAwayFromEdge;

            Assert.That(_environment.Neighbours.Count(), Is.EqualTo(expected));
        }

        private void GivenEnvironmentAtCornerOfGrid(int cornerNumber)
        {
            GivenCorner(cornerNumber);
            _environment = new EdgedEnvironment(Row, Column, Grid);
        }

        private void GivenEnvironmentAtSideOfGrid(int sideNumber)
        {
            GivenSide(sideNumber);
            _environment = new EdgedEnvironment(Row, Column, Grid);
        }

        private void GivenEnvironmentAwayFromEdgeOfGrid()
        {
            GivenColumnAwayFromEdgeOfGrid();
            GivenRowAwayFromEdgeOfGrid();
            _environment = new EdgedEnvironment(Row, Column, Grid);
        }
    }
}
