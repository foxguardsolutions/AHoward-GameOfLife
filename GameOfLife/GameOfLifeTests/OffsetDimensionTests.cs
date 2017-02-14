using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class OffsetDimensionTests : DimensionTests
    {
        [Test]
        public void GetHighNeighborValues_GivenDimensionSizeOfOne_ReturnsOnlyValue()
        {
            var dimension = GivenOffsetDimensionOfSizeOne();
            var expectedNeighbors = new uint[] { dimension.Min };

            var highNeighbors = dimension.GetHighNeighborValues(dimension.Min);

            Assert.That(highNeighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetHighNeighborValuesOfMaxValue_GivenWrapping_ReturnsMaxAndMinValues()
        {
            var dimension = GivenOffsetDimensionWithWrapping();
            var expectedNeighbors = new uint[] { dimension.Min, dimension.Max };

            var highNeighbors = dimension.GetHighNeighborValues(dimension.Max);

            Assert.That(highNeighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetHighNeighborValuesOfMaxValue_GivenNoWrapping_ReturnsOnlyMaxValue()
        {
            var dimension = GivenOffsetDimensionWithoutWrapping();
            var expectedNeighbors = new uint[] { dimension.Max };

            var highNeighbors = dimension.GetHighNeighborValues(dimension.Max);

            Assert.That(highNeighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetHighNeighborValues_GivenValueLessThanMaxAndGreaterThanOrEqualToMin_ReturnsValueAndValuePlusOne()
        {
            var dimension = GivenOffsetDimensionOfSizeGreaterThanOne();
            var valueLessThanMax = (uint)Fixture.CreateInRange((int)dimension.Min, (int)dimension.Max - 1);

            var expectedNeighbors = new uint[] { valueLessThanMax, valueLessThanMax + 1 };

            var highNeighbors = dimension.GetHighNeighborValues(valueLessThanMax);

            Assert.That(highNeighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetHighNeighborValues_GivenValueOutsideDimensionRange_ReturnsNoValues()
        {
            var dimension = Fixture.Create<OffsetDimension>();
            var valueOutsideRange = dimension.Max + Fixture.Create<uint>();
            var expectedNeighbors = new uint[0];

            var highNeighbors = dimension.GetHighNeighborValues(valueOutsideRange);

            Assert.That(highNeighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetLowNeighborValues_GivenDimensionSizeOfOne_ReturnsOnlyValue()
        {
            var dimension = GivenOffsetDimensionOfSizeOne();
            var expectedNeighbors = new uint[] { dimension.Min };

            var lowNeighbors = dimension.GetLowNeighborValues(dimension.Min);

            Assert.That(lowNeighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetLowNeighborValuesOfMinValue_GivenWrapping_ReturnsMinAndMaxValues()
        {
            var dimension = GivenOffsetDimensionWithWrapping();
            var expectedNeigbors = new uint[] { dimension.Min, dimension.Max };

            var lowNeighbors = dimension.GetLowNeighborValues(dimension.Min);

            Assert.That(lowNeighbors, Is.EquivalentTo(expectedNeigbors));
        }

        [Test]
        public void GetLowNeighborValuesOfMinValue_GivenNoWrapping_ReturnsOnlyMinValue()
        {
            Fixture.Register(() => false);
            var dimension = Fixture.Create<OffsetDimension>();
            var expectedNeigbors = new uint[] { dimension.Min };

            var lowNeighbors = dimension.GetLowNeighborValues(dimension.Min);

            Assert.That(lowNeighbors, Is.EquivalentTo(expectedNeigbors));
        }

        [Test]
        public void GetLowNeighborValues_GivenValueGreaterThanMinAndLessThanOrEqualToMax_ReturnsValueAndValueMinusOne()
        {
            var dimension = GivenOffsetDimensionOfSizeGreaterThanOne();
            var valueGreaterThanMin = (uint)Fixture.CreateInRange((int)dimension.Min + 1, (int)dimension.Max);

            var expectedNeighbors = new uint[] { valueGreaterThanMin - 1, valueGreaterThanMin };

            var lowNeighbors = dimension.GetLowNeighborValues(valueGreaterThanMin);

            Assert.That(lowNeighbors, Is.EquivalentTo(expectedNeighbors));
        }

        [Test]
        public void GetLowNeighborValues_GivenValueOutsideDimensionRange_ReturnsNoValues()
        {
            var dimension = Fixture.Create<OffsetDimension>();
            var valueOutsideRange = dimension.Max + Fixture.Create<uint>();
            var expectedNeighbors = new uint[0];

            var lowNeighbors = dimension.GetLowNeighborValues(valueOutsideRange);

            Assert.That(lowNeighbors, Is.EquivalentTo(expectedNeighbors));
        }

        private OffsetDimension GivenOffsetDimensionOfSizeOne()
        {
            Fixture.Register<uint>(() => 1);
            return Fixture.Create<OffsetDimension>();
        }

        private OffsetDimension GivenOffsetDimensionOfSizeGreaterThanOne()
        {
            var sizeGreaterThanOne = 1 + Fixture.Create<uint>();
            Fixture.Register(() => sizeGreaterThanOne);
            return Fixture.Create<OffsetDimension>();
        }

        private OffsetDimension GivenOffsetDimensionWithWrapping()
        {
            Fixture.Register(() => true);
            return Fixture.Create<OffsetDimension>();
        }

        private OffsetDimension GivenOffsetDimensionWithoutWrapping()
        {
            Fixture.Register(() => false);
            return Fixture.Create<OffsetDimension>();
        }
    }
}
