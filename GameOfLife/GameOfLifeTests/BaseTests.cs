using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    [TestFixture]
    public class BaseTests
    {
        protected Fixture Fixture { get; private set; }

        [SetUp]
        public void FixtureSetUp()
        {
            Fixture = new Fixture();
            Fixture.Register(() =>
            {
                var ruleFactory = new RuleFactory();
                return ruleFactory.Create();
            });
        }
    }
}
