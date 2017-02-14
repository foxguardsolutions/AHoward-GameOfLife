using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class DimensionTests : BaseTests
    {
        [Test]
        public void IsOwnNeighbor_GivenDimensionSizeOfOne_ReturnsWrappingStatus()
        {
            var wraps = Fixture.Freeze<bool>();
            var dimension = GivenDimensionOfSizeOne();

            var isOwnNeighbor = dimension.IsOwnNeighbor();

            Assert.That(isOwnNeighbor, Is.EqualTo(wraps));
        }

        [Test]
        public void IsOwnNeighbor_GivenDimensionSizeGreaterThanOne_ReturnsFalse()
        {
            var dimension = GivenDimensionOfSizeGreaterThanOne();

            var isOwnNeighbor = dimension.IsOwnNeighbor();

            Assert.That(isOwnNeighbor, Is.False);
        }

        [Test]
        public void GetNeighborValues_GivenValueGreaterThanMinAndLessThanMax_ReturnsValueAndValuesOnEitherSide()
        {
            var dimension = GivenDimensionWithAtLeastOneValueBetweenMinAndMax();
            var internalValue = (uint)Fixture.CreateInRange((int)dimension.Min + 1, (int)dimension.Max - 1);

            var expectedNeighbors = new uint[] { internalValue - 1, internalValue, internalValue + 1 };

            var neighbors = dimension.GetNeighborValues(internalValue);

            Assert.That(neighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetNeighborValuesOfMinValue_GivenWrapping_ReturnsMaxMinAndMinPlusOne()
        {
            var dimension = GivenDimensionWithWrapping();
            var expectedNeighbors = new uint[] { dimension.Min, dimension.Min + 1, dimension.Max };

            var neighbors = dimension.GetNeighborValues(dimension.Min);

            Assert.That(neighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetNeighborValuesOfMinValue_GivenNoWrapping_ReturnsMinAndMinPlusOne()
        {
            var dimension = GivenDimensionWithoutWrapping();
            var expectedNeighbors = new uint[] { dimension.Min, dimension.Min + 1 };

            var neighbors = dimension.GetNeighborValues(dimension.Min);

            Assert.That(neighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetNeighborValuesOfMaxValue_GivenWrapping_ReturnsMinMaxAndMaxMinusOne()
        {
            var dimension = GivenDimensionWithWrapping();
            var expectedNeighbors = new uint[] { dimension.Min, dimension.Max, dimension.Max - 1 };

            var neighbors = dimension.GetNeighborValues(dimension.Max);

            Assert.That(neighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetNeighborValuesOfMaxValue_GivenNoWrapping_ReturnsMaxAndMaxMinusOne()
        {
            var dimension = GivenDimensionWithoutWrapping();
            var expectedNeighbors = new uint[] { dimension.Max, dimension.Max - 1 };

            var neighbors = dimension.GetNeighborValues(dimension.Max);

            Assert.That(neighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetNeighborValues_GivenDimensionSizeOfOne_ReturnsOnlyValue()
        {
            var dimension = GivenDimensionOfSizeOne();
            var expectedNeighbors = new uint[] { dimension.Min };

            var neighbors = dimension.GetNeighborValues(dimension.Min);

            Assert.That(neighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetNeighborValues_GivenValueOutsideDimensionRange_ReturnsNoValues()
        {
            var dimension = Fixture.Create<Dimension>();
            var valueOutsideRange = dimension.Max + Fixture.Create<uint>();
            var expectedNeighbors = new uint[0];

            var neighbors = dimension.GetNeighborValues(valueOutsideRange);

            Assert.That(neighbors, Is.EquivalentTo(expectedNeighbors));
        }

        private Dimension GivenDimensionOfSizeOne()
        {
            Fixture.Register<uint>(() => 1);
            return Fixture.Create<Dimension>();
        }

        private Dimension GivenDimensionOfSizeGreaterThanOne()
        {
            var sizeGreaterThanOne = 1 + Fixture.Create<uint>();
            Fixture.Register(() => sizeGreaterThanOne);
            return Fixture.Create<Dimension>();
        }

        private Dimension GivenDimensionWithAtLeastOneValueBetweenMinAndMax()
        {
            var sizeWithAtLeastOneValueBetweenMinAndMax = 1 + Fixture.Create<uint>() + 1;
            Fixture.Register(() => sizeWithAtLeastOneValueBetweenMinAndMax);
            return Fixture.Create<Dimension>();
        }

        private Dimension GivenDimensionWithWrapping()
        {
            Fixture.Register(() => true);
            return Fixture.Create<Dimension>();
        }

        private Dimension GivenDimensionWithoutWrapping()
        {
            Fixture.Register(() => false);
            return Fixture.Create<Dimension>();
        }
    }
}
