using System.Linq;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public static class AutoFixtureExtension
    {
        public static T[][] CreateRectangularJaggedArray<T>(this IFixture fixture)
        {
            var numberOfRows = fixture.Create<int>();
            var numberOfColumns = fixture.Create<int>();

            return fixture.CreateRectangularJaggedArray<T>(numberOfRows, numberOfColumns);
        }

        public static T[][] CreateRectangularJaggedArray<T>(this IFixture fixture, int numberOfRows, int numberOfColumns)
        {
            var array = new T[numberOfRows][];

            for (int rowNumber = 0; rowNumber < numberOfRows; rowNumber++)
                array[rowNumber] = fixture.CreateMany<T>(numberOfColumns).ToArray();

            return array;
        }

        public static long CreateInRange(this IFixture fixture, int lowerLimit, int upperLimit)
        {
            if (lowerLimit == upperLimit)
                return lowerLimit;

            fixture.Customizations.Add(new RandomNumericSequenceGenerator(lowerLimit, upperLimit));
            var value = fixture.Create<long>();
            fixture.Customizations.RemoveAt(fixture.Customizations.Count - 1);
            return value;
        }

        public static T PickFromValues<T>(this IFixture fixture, params T[] collection)
        {
            var elementIndex = fixture.CreateInRange(0, collection.Length - 1);
            return collection[elementIndex];
        }
    }
}
