using System;
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
        public void NewRuleset_CreatesRuleset()
        {
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
                _ruleset.Set(state, _rule);

            var completeStatus = _ruleset.IsComplete();

            Assert.That(completeStatus, Is.True);
        }

        [Test]
        public void Apply_CallsRuleApplyMethod()
        {
            var number = Fixture.Create<uint>();
            var expectedFinalState = Fixture.Create<LifeState>();
            _mockRule.Setup(r => r.Apply(number)).Returns(expectedFinalState);

            var initialState = Fixture.Create<LifeState>();
            _ruleset.Set(initialState, _rule);

            var actualFinalState = _ruleset.Apply(initialState, number);

            Assert.That(actualFinalState, Is.EqualTo(expectedFinalState));
        }
    }
}
