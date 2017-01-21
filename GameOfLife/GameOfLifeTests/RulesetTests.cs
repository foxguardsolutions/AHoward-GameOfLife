using System;
using System.Collections.Generic;
using System.Linq;
using GameOfLife;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class RulesetTests : BaseTests
    {
        private Mock<IRule> _mockRule;
        private IRule _rule;
        private Ruleset _ruleset;

        [SetUp]
        public void SetUp()
        {
            _mockRule = new Mock<IRule>();
            _rule = _mockRule.Object;
            _ruleset = new Ruleset();
        }

        [Test]
        public void IsComplete_WithoutRulesEstablishedForAllStates_ReturnsFalse()
        {
            var completeStatus = _ruleset.IsComplete();

            Assert.That(completeStatus, Is.False);
        }

        [Test]
        public void IsComplete_WithRulesEstablishedForAllStates_ReturnsTrue()
        {
            foreach (LifeState state in Enum.GetValues(typeof(LifeState)))
                _ruleset[state] = _rule;

            var completeStatus = _ruleset.IsComplete();

            Assert.That(completeStatus, Is.True);
        }

        [Test]
        public void SetNextState_SetsCellStateToResultOfRuleApplication()
        {
            var initialState = Fixture.Create<LifeState>();
            var cell = new Cell(initialState);
            var neighbors = Fixture.CreateMany<Cell>().ToArray();
            var expectedFinalState = Fixture.CreateUnequalToDefault<LifeState>();
            SetUpMockRule(neighbors, expectedFinalState, initialState);

            _ruleset.SetNextState(cell, neighbors);
            var actualFinalState = GetNextState(cell);

            Assert.That(actualFinalState, Is.EqualTo(expectedFinalState));
        }

        private void SetUpMockRule(IEnumerable<Cell> cells, LifeState expectedFinalState, LifeState initialState)
        {
            _mockRule.Setup(r => r.Apply(cells)).Returns(expectedFinalState);
            _ruleset[initialState] = _rule;
        }

        private LifeState GetNextState(Cell cell)
        {
            cell.AdvanceState();
            return cell.CurrentState;
        }
    }
}
