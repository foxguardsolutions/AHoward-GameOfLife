using GameOfLife.Environments;
using NUnit.Framework;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Tests.Environments
{
    [TestFixture]
    public class CellEnvironmentFactoryTests : TestsUsingEnvironments
    {
        private CellEnvironmentFactory _factory;

        [SetUp]
        public new void SetUp()
        {
            _factory = Fixture.Create<CellEnvironmentFactory>();
        }

        [TestCaseSource(nameof(WrappingParametersAndExpectedTypes))]
        public void GetEnvironment_GivenWrappingParameter_ReturnsExpectedEnvironment(bool wrap, Type expectedType)
        {
            _factory = new CellEnvironmentFactory(wrap);

            var actual = _factory.GetEnvironment(Row, Column, Grid);

            Assert.That(actual, Is.TypeOf(expectedType));
        }

        [Test]
        public void GetEnvironments_GivenGrid_ReturnsEnvironmentForEachCellOnGrid()
        {
            var expectedCount = Grid.Height * Grid.Width;

            var actual = _factory.GetEnvironments(Grid);

            Assert.That(actual.Count(), Is.EqualTo(expectedCount));
        }

        private static IEnumerable<TestCaseData> WrappingParametersAndExpectedTypes()
        {
            yield return new TestCaseData(false, typeof(EdgedEnvironment));
            yield return new TestCaseData(true, typeof(WrappedEnvironment));
        }
    }
}
