using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class DimensionTests : BaseTests
    {
        private bool _wraps;
        private uint _size;

        [SetUp]
        public void SetUp()
        {
            _wraps = Fixture.Create<bool>();
            _size = Fixture.Create<uint>();
        }

        [Test]
        public void IsOwnNeighbor_GivenDimensionSizeOfOne_ReturnsWrappingStatus()
        {
            var dimension = new Dimension(1, _wraps);

            var isOwnNeighbor = dimension.IsOwnNeighbor();

            Assert.That(isOwnNeighbor, Is.EqualTo(_wraps));
        }

        [Test]
        public void IsOwnNeighbor_GivenDimensionSizeGreaterThanOne_ReturnsFalse()
        {
            var sizeGreaterThanOne = 1 + _size;
            var dimension = new Dimension(sizeGreaterThanOne, _wraps);

            var isOwnNeighbor = dimension.IsOwnNeighbor();

            Assert.That(isOwnNeighbor, Is.False);
        }

        [Test]
        public void GetNeighborValues_GivenValueGreaterThanMinAndLessThanMax_ReturnsValueAndValuesOnEitherSide()
        {
            var dimension = new Dimension(_size, _wraps);
            var internalValue = Fixture.CreateInRange<uint>((int)dimension.Min + 1, (int)dimension.Max - 1);

            var expectedNeighbors = new uint[] { internalValue - 1, internalValue, internalValue + 1 };

            var neighbors = dimension.GetNeighborValues(internalValue);

            Assert.That(neighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetNeighborValuesOfMinValue_GivenWrapping_ReturnsMaxMinAndMinPlusOne()
        {
            var wraps = true;
            var dimension = new Dimension(_size, wraps);
            var expectedNeighbors = new uint[] { dimension.Min, dimension.Min + 1, dimension.Max };

            var neighbors = dimension.GetNeighborValues(dimension.Min);

            Assert.That(neighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetNeighborValuesOfMinValue_GivenNoWrapping_ReturnsMinAndMinPlusOne()
        {
            var wraps = false;
            var dimension = new Dimension(_size, wraps);
            var expectedNeighbors = new uint[] { dimension.Min, dimension.Min + 1 };

            var neighbors = dimension.GetNeighborValues(dimension.Min);

            Assert.That(neighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetNeighborValuesOfMaxValue_GivenWrapping_ReturnsMinMaxAndMaxMinusOne()
        {
            var wraps = true;
            var dimension = new Dimension(_size, wraps);
            var expectedNeighbors = new uint[] { dimension.Min, dimension.Max, dimension.Max - 1 };

            var neighbors = dimension.GetNeighborValues(dimension.Max);

            Assert.That(neighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetNeighborValuesOfMaxValue_GivenNoWrapping_ReturnsMaxAndMaxMinusOne()
        {
            var wraps = false;
            var dimension = new Dimension(_size, wraps);
            var expectedNeighbors = new uint[] { dimension.Max, dimension.Max - 1 };

            var neighbors = dimension.GetNeighborValues(dimension.Max);

            Assert.That(neighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetNeighborValues_GivenDimensionSizeOfOne_ReturnsOnlyValue()
        {
            var dimension = new Dimension(1, _wraps);
            var expectedNeighbors = new uint[] { dimension.Min };

            var neighbors = dimension.GetNeighborValues(dimension.Min);

            Assert.That(neighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetNeighborValues_GivenValueOutsideDimensionRange_ReturnsNoValues()
        {
            var dimension = new Dimension(_size, _wraps);
            var valueOutsideRange = dimension.Max + Fixture.Create<uint>();
            var expectedNeighbors = new uint[0];

            var neighbors = dimension.GetNeighborValues(valueOutsideRange);

            Assert.That(neighbors, Is.EquivalentTo(expectedNeighbors));
        }
    }
}
