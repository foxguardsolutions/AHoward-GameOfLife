using System.Collections.Generic;
using System.Linq;
using GameOfLife;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class GameAdvancerChangesGridTests : GameAdvancerTests
    {
        private Mock<IGrid> _mockGrid;

        protected override void SetUpMockConsole()
        {
            Fixture.Freeze<Mock<IConsoleWriter>>();
        }

        [SetUp]
        public void SetUpMockGrid()
        {
            _mockGrid = new Mock<IGrid>(MockBehavior.Strict);
            Fixture.Register(() => _mockGrid.Object);
        }

        [Test]
        public void Step_GivenIncompleteRules_DoesNotIterateOverGrid()
        {
            GivenRulesetWithCompletionStatus(false);

            Advancer.Step(_mockGrid.Object, MockRuleset.Object);
        }

        [Test]
        public void Step_GivenCompleteRules_IteratesOverGridTwice()
        {
            SetUpGridEnumerator();
            GivenRulesetWithCompletionStatus(true);

            Advancer.Step(_mockGrid.Object, MockRuleset.Object);

            _mockGrid.Verify(g => g.GetEnumerator(), Times.Exactly(2));
        }

        private void SetUpGridEnumerator()
        {
            _mockGrid.Setup(g => g.GetEnumerator()).Returns(EmptyEnumerator());
        }

        private IEnumerator<CellPosition> EmptyEnumerator()
        {
            yield break;
        }

        /* TODO: Decide which of these next two tests to keep */

        // Testing method call order by using Moq's Callback method and temporary variables
        [Test]
        public void Step_CalculatesNextStateOfAllCellsBeforeAdvancingAnyCellStates()
        {
            var numberOfTimesNextCellStateHasBeenCalculated = 0;
            MockRuleset.Setup(
                r => r.SetNextStateOfCellGivenNeighbors(It.IsAny<Cell>(), It.IsAny<IEnumerable<Cell>>()))
                .Callback(() => numberOfTimesNextCellStateHasBeenCalculated++);
            GivenRulesetWithCompletionStatus(true);

            var numberOfCalculationsMadeBeforeEachCellStateWasAdvanced = new List<int>();

            var mockCell = Fixture.Create<Mock<Cell>>();
            mockCell.Setup(
                c => c.AdvanceState())
                .Callback(() => numberOfCalculationsMadeBeforeEachCellStateWasAdvanced.Add(numberOfTimesNextCellStateHasBeenCalculated));

            SetUpMockGridContaining(mockCell.Object);
            var totalCellCount = _mockGrid.Object.Count();

            Advancer.Step(_mockGrid.Object, MockRuleset.Object);

            Assert.That(numberOfCalculationsMadeBeforeEachCellStateWasAdvanced, Is.All.EqualTo(totalCellCount));
        }

        private void SetUpMockGridContaining(Cell cell)
        {
            _mockGrid.Setup(g => g.GetCellAt(It.IsAny<CellPosition>()))
                .Returns(cell);
            _mockGrid.Setup(g => g.GetEnumerator())
                .Returns(() => Fixture.CreateMany<CellPosition>().GetEnumerator());
            _mockGrid.Setup(g => g.GetNeighborsOfCellAt(It.IsAny<CellPosition>()))
                .Returns(Fixture.CreateMany<Cell>());
        }

        // Testing using a configuration that will fail if methods are called in the wrong order
        [Test]
        public void Step_CalculatesNextStateOfAllCellsBeforeAdvancingAnyCellStates_UsingPremadeConfigurationThatWouldFail()
        {
            var grid = MockObjects.GridWithAllDeadCellsExceptTwoLiveCellsNeighboringEachOther;
            var rules = MockObjects.RulesThatLeaveAllCellsDeadExceptIsolatedLiveCells;

            Advancer.Step(grid, rules);
            var finalPattern = grid.GetCurrentPattern();

            Assert.That(finalPattern, Is.EqualTo(MockObjects.PatternWithAllDeadCells));
        }
    }
}
