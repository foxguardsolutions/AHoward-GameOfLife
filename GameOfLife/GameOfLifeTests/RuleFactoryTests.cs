using System.Linq;
using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class RuleFactoryTests : BaseTests
    {
        [Test]
        public void Create_ReturnsNewRule()
        {
            var liveNeighborCount = Fixture.CreateMany<uint>().ToArray();
            var ruleFactory = new RuleFactory();
            var rule = ruleFactory.Create(liveNeighborCount);
            Assert.That(rule, Is.TypeOf(typeof(Rule)));
        }
    }
}
