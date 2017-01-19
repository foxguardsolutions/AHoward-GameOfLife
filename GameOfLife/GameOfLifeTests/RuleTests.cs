using System.Linq;
using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class RuleTests : BaseTests
    {
        private uint[] _liveNeighborCount;
        private RuleFactory _ruleFactory;

        [SetUp]
        public void SetUp()
        {
            _liveNeighborCount = Fixture.CreateMany<uint>().ToArray();
            _ruleFactory = Fixture.Create<RuleFactory>();
        }

        [Test]
        public void Apply_GivenLiveNeighborCountInRuleCountsArray_ReturnsAlive()
        {
            var rule = _ruleFactory.Create(_liveNeighborCount);
            var ruleApplication = rule.Apply(_liveNeighborCount[0]);

            Assert.That(ruleApplication, Is.EqualTo(LifeState.Alive));
        }

        [Test]
        public void Apply_GivenLiveNeighborCountNotInRuleCountsArray_ReturnsDead()
        {
            var rule = _ruleFactory.Create();
            var ruleApplication = rule.Apply(_liveNeighborCount[0]);

            Assert.That(ruleApplication, Is.EqualTo(LifeState.Dead));
        }
    }
}
