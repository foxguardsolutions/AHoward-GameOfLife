using System.Linq;
using GameOfLife.EnvironmentRules;
using GameOfLife.Environments;
using GameOfLife.Operations;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLife.Tests.EnvionmentRules
{
    [TestFixture]
    public class GameOfLifeRulesTests : TestsUsingGrid
    {
        private const int MaximumNeighboursToLive = GameOfLifeRules.MaximumNeighboursToLive;
        private const int MinimumNeighboursToLive = GameOfLifeRules.MinimumNeighboursToLive;

        private CellEnvironment _environment;
        private GameOfLifeRules _analyzer;

        [SetUp]
        public new void SetUp()
        {
            _environment = new EdgedEnvironment(1, 1, Grid);
            _analyzer = new GameOfLifeRules();
        }

        [Test]
        public void GetOperationToPerformOnCell_GivenDeadCellSurroundedByMaximumNeighboursToLive_ReturnsBringToLife()
        {
            GivenCellSurroundedByNumberOfLivingNeighbours(false, MaximumNeighboursToLive);
            var expected = CellOperation.BringToLife;

            var actual = _analyzer.GetOperationToPerformOnCell(_environment);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetOperationToPerformOnCell_GivenDeadCellSurroundedByMoreOrLessThanMaximumNeighboursToLive_ReturnsNoAction()
        {
            var numberOfLivingNeighbours = GetNumberOfNeighboursOtherThanValue(MaximumNeighboursToLive);
            GivenCellSurroundedByNumberOfLivingNeighbours(false, numberOfLivingNeighbours);
            var expected = CellOperation.NoAction;

            var actual = _analyzer.GetOperationToPerformOnCell(_environment);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(BooleanValues))]
        public void GetOperationToPerformOnCell_GivenDeadCellSurroundedByUniformNeighbours_ReturnsNoAction(bool liveNeighbours)
        {
            var numberOfLivingNeighbours = liveNeighbours ? _environment.Neighbours.Count() : 0;
            GivenCellSurroundedByNumberOfLivingNeighbours(false, numberOfLivingNeighbours);
            var expected = CellOperation.NoAction;

            var actual = _analyzer.GetOperationToPerformOnCell(_environment);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetOperationToPerformOnCell_GivenLiveCellSurroundedByANumberOfNeighboursMoreOrLessThanNeededToLive_ReturnsKill()
        {
            var numberOfLivingNeighbours = GetNumberOfNeighboursOutsideOfValues(MinimumNeighboursToLive, MaximumNeighboursToLive);
            GivenCellSurroundedByNumberOfLivingNeighbours(true, numberOfLivingNeighbours);
            var expected = CellOperation.Kill;

            var actual = _analyzer.GetOperationToPerformOnCell(_environment);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetOperationToPerformOnCell_GivenLiveCellSurroundedByANumberOfNeighboursNeededToLive_ReturnsNoAction()
        {
            var numberOfLivingNeighbours = GetNumberOfNeighboursBetweenValues(MinimumNeighboursToLive, MaximumNeighboursToLive);
            GivenCellSurroundedByNumberOfLivingNeighbours(true, numberOfLivingNeighbours);
            var expected = CellOperation.NoAction;

            var actual = _analyzer.GetOperationToPerformOnCell(_environment);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(BooleanValues))]
        public void GetOperationToPerformOnCell_GivenLiveCellSurroundedByUniformNeighbours_ReturnsKill(bool liveNeighbours)
        {
            var numberOfLivingNeighbours = liveNeighbours ? _environment.Neighbours.Count() : 0;
            GivenCellSurroundedByNumberOfLivingNeighbours(true, numberOfLivingNeighbours);
            var expected = CellOperation.Kill;

            var actual = _analyzer.GetOperationToPerformOnCell(_environment);

            Assert.That(actual, Is.EqualTo(expected));
        }

        private int GetNumberOfNeighboursBetweenValues(int minimumValue, int maximumValue)
        {
            var valueRange = maximumValue - minimumValue + 1;
            var value = Fixture.Create<int>() % valueRange;
            return value + minimumValue;
        }

        private int GetNumberOfNeighboursOtherThanValue(int value)
        {
            var numberOfNeighbours = Fixture.Create<int>() % _environment.Neighbours.Count();
            if (numberOfNeighbours == value) numberOfNeighbours++;
            return numberOfNeighbours;
        }

        private int GetNumberOfNeighboursOutsideOfValues(int minimumValue, int maximumValue)
        {
            var numberOfNeighbours = Fixture.Create<int>() % _environment.Neighbours.Count();
            while (minimumValue <= numberOfNeighbours && numberOfNeighbours <= maximumValue)
                numberOfNeighbours++;
            return numberOfNeighbours;
        }

        private void GivenCellSurroundedByNumberOfLivingNeighbours(bool liveCell, int numberOfLivingNeighbours)
        {
            GivenEnvironmentOfCell(liveCell);
            GivenNumberOfLivingNeighbours(numberOfLivingNeighbours);
        }

        private void GivenEnvironmentOfCell(bool alive)
        {
            _environment.Cell.Alive = alive;
        }

        private void GivenNumberOfLivingNeighbours(int livingNeighbours)
        {
            var neighboursToLive = _environment.Neighbours.Take(livingNeighbours);
            foreach (var cell in neighboursToLive)
                cell.Alive = true;
        }
    }
}
