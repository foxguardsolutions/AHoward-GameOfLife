using NUnit.Framework;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using GameOfLife.EnvironmentRules;
using GameOfLife.Environments;

namespace GameOfLife.Tests
{
    [TestFixture]
    public class GameTests : TestsUsingGrid
    {
        private int _generation;
        private CellEnvironmentFactory _factory;
        private Game _game;
        private IEnvironmentRules _rules;

        [SetUp]
        public new void SetUp()
        {
            _generation = Fixture.Create<int>();
            _factory = Fixture.Create<CellEnvironmentFactory>();
            _rules = new GameOfLifeRules();
        }

        [TestCaseSource(nameof(OscillatingGrids))]
        public void Proceed_GivenOscillatingGrid_ChangesGridToExpectedState(Grid oscillatingGrid, Grid expectedState)
        {
            _game = new Game(_generation, oscillatingGrid, _rules, _factory);
            var expected = GetCellStatuses(expectedState);

            _game.Proceed();

            Assert.That(GetCellStatuses(_game.Grid), Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(StillLifeGrids))]
        public void Proceed_GivenStillLifeGrid_AppliesGameRulesAppropriately(Grid stillLifeGrid)
        {
            _game = new Game(_generation, stillLifeGrid, _rules, _factory);
            var expected = GetCellStatuses(stillLifeGrid);

            _game.Proceed();

            Assert.That(GetCellStatuses(_game.Grid), Is.EqualTo(expected));
        }
        
        [Test]
        public void Proceed_IncreasesGenerationBy1()
        {
            _game = new Game(_generation, Grid, _rules, _factory);
            var expected = _generation + 1;

            _game.Proceed();

            Assert.That(_game.Generation, Is.EqualTo(expected));
        }

        private static Grid GetBeehiveGrid()
        {
            return GridFactory.Parse("5 6"
                         + $"{NewLine}......"
                         + $"{NewLine}..**.."
                         + $"{NewLine}.*..*."
                         + $"{NewLine}..**.."
                         + $"{NewLine}......");
        }

        private static Grid GetBlinkerStage1Grid()
        {
            return GridFactory.Parse("5 5"
                         + $"{NewLine}....."
                         + $"{NewLine}....."
                         + $"{NewLine}.***."
                         + $"{NewLine}....."
                         + $"{NewLine}.....");
        }

        private static Grid GetBlinkerStage2Grid()
        {
            return GridFactory.Parse("5 5"
                         + $"{NewLine}....."
                         + $"{NewLine}..*.."
                         + $"{NewLine}..*.."
                         + $"{NewLine}..*.."
                         + $"{NewLine}.....");
        }

        private static Grid GetBlockGrid()
        {
            return GridFactory.Parse("4 4"
                         + $"{NewLine}...."
                         + $"{NewLine}.**."
                         + $"{NewLine}.**."
                         + $"{NewLine}....");
        }

        private static Grid GetToadStage1Grid()
        {
            return GridFactory.Parse("6 6"
                         + $"{NewLine}......"
                         + $"{NewLine}......"
                         + $"{NewLine}..***."
                         + $"{NewLine}.***.."
                         + $"{NewLine}......"
                         + $"{NewLine}......");
        }

        private static Grid GetToadStage2Grid()
        {
            return GridFactory.Parse("6 6"
                         + $"{NewLine}......"
                         + $"{NewLine}...*.."
                         + $"{NewLine}.*..*."
                         + $"{NewLine}.*..*."
                         + $"{NewLine}..*..."
                         + $"{NewLine}......");
        }

        private static Grid GetTubGrid()
        {
            return GridFactory.Parse("5 5"
                         + $"{NewLine}....."
                         + $"{NewLine}..*.."
                         + $"{NewLine}.*.*."
                         + $"{NewLine}..*.."
                         + $"{NewLine}.....");
        }

        private static IEnumerable<TestCaseData> OscillatingGrids()
        {
            yield return new TestCaseData(GetBlinkerStage1Grid(), GetBlinkerStage2Grid());
            yield return new TestCaseData(GetBlinkerStage2Grid(), GetBlinkerStage1Grid());
            yield return new TestCaseData(GetToadStage1Grid(), GetToadStage2Grid());
            yield return new TestCaseData(GetToadStage2Grid(), GetToadStage1Grid());
        }

        private static IEnumerable<Grid> StillLifeGrids()
        {
            yield return GetBlockGrid();
            yield return GetBeehiveGrid();
            yield return GetTubGrid();
        }
    }
}
