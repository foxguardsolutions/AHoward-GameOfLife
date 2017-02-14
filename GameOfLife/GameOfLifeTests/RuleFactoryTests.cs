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
            var ruleFactory = new RuleFactory();
            var numbers = Fixture.CreateMany<uint>().ToArray();
            var rule = ruleFactory.CreateRule(numbers);
            Assert.That(rule, Is.TypeOf(typeof(Rule)));
        }
    }
}
