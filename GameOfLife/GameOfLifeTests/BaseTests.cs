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
        }
    }
}
