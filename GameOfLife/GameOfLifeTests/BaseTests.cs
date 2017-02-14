using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace GameOfLifeTests
{
    [TestFixture]
    public class BaseTests
    {
        protected IFixture Fixture { get; private set; }

        [SetUp]
        public void FixtureSetUp()
        {
            Fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
        }
    }
}
