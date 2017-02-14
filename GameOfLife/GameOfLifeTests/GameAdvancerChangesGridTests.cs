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

        [Test]
        public void Step_CalculatesNextStateOfAllCellsBeforeAdvancingAnyCellStates_UsingPremadeConfigurationThatWouldFail()
        {
            var grid = GridWithAllDeadCellsExceptTwoLiveCellsNeighboringEachOther;
            var rules = RulesThatLeaveAllCellsDeadExceptIsolatedLiveCells;

            Advancer.Step(grid, rules);
            var finalPattern = grid.GetCurrentPattern();

            Assert.That(finalPattern, Is.EqualTo(PatternWithAllDeadCells));
        }

        private IRuleset RulesThatLeaveAllCellsDeadExceptIsolatedLiveCells
        {
            get
            {
                var rules = new Ruleset(new RuleFactory());
                rules.SetRuleFor(LifeState.Alive, 0);
                rules.SetRuleFor(LifeState.Dead);
                return rules;
            }
        }

        private IGrid GridWithAllDeadCellsExceptTwoLiveCellsNeighboringEachOther
        {
            get
            {
                var grid = new SquareTileGrid(MakeDeadCells(3, 4), false, false);
                foreach (var position in TwoPositionsNeighboringEachOther())
                    SetToAlive(grid.GetCellAt(position));

                return grid;
            }
        }

        private IEnumerable<CellPosition> TwoPositionsNeighboringEachOther()
        {
            yield return new CellPosition(1, 1);
            yield return new CellPosition(1, 2);
        }

        private IEnumerable<LifeState> PatternWithAllDeadCells
        {
            get { return GridWithAllDeadCells.GetCurrentPattern(); }
        }

        private IGrid GridWithAllDeadCells
        {
            get { return new SquareTileGrid(MakeDeadCells(3, 4), false, false); }
        }

        private void SetToAlive(Cell cell)
        {
            cell.SetNextState(LifeState.Alive);
            cell.AdvanceState();
        }

        private Cell[][] MakeDeadCells(uint numberOfRows, uint numberOfColumns)
        {
            var cells = new Cell[numberOfRows][];
            for (int row = 0; row < numberOfRows; row++)
                cells[row] = MakeDeadCells(numberOfColumns).ToArray();

            return cells;
        }

        private IEnumerable<Cell> MakeDeadCells(uint numberOfCells)
        {
            for (int numberAlreadyMade = 0; numberAlreadyMade < numberOfCells; numberAlreadyMade++)
                yield return new Cell(LifeState.Dead);
        }
    }
}
