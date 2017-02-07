using System;
using System.Collections.Generic;
using GameOfLife;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class RulesetTests : BaseTests
    {
        private Mock<IRule> _mockRule;
        private Mock<IRuleFactory> _mockRuleFactory;
        private Ruleset _ruleset;

        [SetUp]
        public void SetUp()
        {
            _mockRule = Fixture.Freeze<Mock<IRule>>();
            _mockRuleFactory = Fixture.Freeze<Mock<IRuleFactory>>();

            _ruleset = Fixture.Create<Ruleset>();
        }

        [Test]
        public void SetRuleFor_GivenRuleParameters_CreatesAndStoresNewRuleInRuleset()
        {
            var state = Fixture.Create<LifeState>();
            var numbersYieldingLive = Fixture.Create<uint[]>();
            _mockRuleFactory.Setup(r => r.CreateRule(numbersYieldingLive)).Returns(_mockRule.Object);

            _ruleset.SetRuleFor(state, numbersYieldingLive);

            _mockRuleFactory.Verify(r => r.CreateRule(numbersYieldingLive));
            Assert.That(_ruleset.Rules[state], Is.EqualTo(_mockRule.Object));
        }

        [Test]
        public void SetDefaultRules_CreatesAndStoresDefaultRules()
        {
            var survivalRule = SetUpMockRuleFor(DefaultSettings.SurvivalNumbers);
            var reproductionRule = SetUpMockRuleFor(DefaultSettings.ReproductionNumbers);

            _ruleset.SetDefaultRules();

            Assert.That(_ruleset.Rules[LifeState.Alive], Is.EqualTo(survivalRule));
            Assert.That(_ruleset.Rules[LifeState.Dead], Is.EqualTo(reproductionRule));
        }

        private IRule SetUpMockRuleFor(uint[] neighborCounts)
        {
            var mockRule = new Mock<IRule>();
            _mockRuleFactory.Setup(
                r => r.CreateRule(neighborCounts))
                .Returns(mockRule.Object);
            return mockRule.Object;
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
                _ruleset.SetRuleFor(state);

            var completeStatus = _ruleset.IsComplete();

            Assert.That(completeStatus, Is.True);
        }

        [Test]
        public void SetNextState_SetsCellStateToResultOfRuleApplication()
        {
            var cell = Fixture.Create<Cell>();
            var neighbors = Fixture.CreateMany<Cell>();
            var expectedFinalState = Fixture.Create<LifeState>();
            SetUpMockRule(cell, neighbors, expectedFinalState);

            _ruleset.SetNextStateOfCellGivenNeighbors(cell, neighbors);
            var actualFinalState = GetNextState(cell);

            Assert.That(actualFinalState, Is.EqualTo(expectedFinalState));
        }

        private void SetUpMockRule(Cell cell, IEnumerable<Cell> cells, LifeState expectedFinalState)
        {
            _ruleset.SetRuleFor(cell.CurrentState);
            _mockRule.Setup(r => r.Apply(cells)).Returns(expectedFinalState);
        }

        private LifeState GetNextState(Cell cell)
        {
            cell.AdvanceState();
            return cell.CurrentState;
        }
    }
}
