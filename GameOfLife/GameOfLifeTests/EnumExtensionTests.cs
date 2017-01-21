using System;
using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;
using static GameOfLife.EnumExtension;

namespace GameOfLifeTests
{
    public class EnumExtensionTests : BaseTests
    {
        [Test]
        public void ForEvery_GivenPredicateThatReturnsTrueForAll_ReturnsTrue()
        {
            Func<LifeState, bool> returnTrue = s => true;

            var returnValue = ForEvery(returnTrue);

            Assert.That(returnValue, Is.True);
        }

        [Test]
        public void ForEvery_GivenPredicateThatReturnsFalseForAtLeastOne_ReturnsFalse()
        {
            var someLifeState = Fixture.Create<LifeState>();
            Func<LifeState, bool> returnFalseForSomeLifeState = s => s != someLifeState;

            var returnValue = ForEvery(returnFalseForSomeLifeState);

            Assert.That(returnValue, Is.False);
        }
    }
}
