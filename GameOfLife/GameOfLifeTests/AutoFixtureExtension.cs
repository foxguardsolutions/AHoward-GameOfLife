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

        public static T CreateInRange<T>(this IFixture fixture, int lowerLimit, int upperLimit)
        {
            fixture.Customizations.Add(new RandomNumericSequenceGenerator(lowerLimit, upperLimit));
            var value = fixture.Create<T>();
            fixture.Customizations.RemoveAt(fixture.Customizations.Count - 1);
            return value;
        }

        public static T CreateUnequalToDefault<T>(this IFixture fixture)
        {
            var excludeItem = default(T);
            T newItem;

            do
                newItem = fixture.Create<T>();
            while (newItem.Equals(excludeItem));

            return newItem;
        }

        public static T PickFromValues<T>(this IFixture fixture, params T[] collection)
        {
            var elementIndex = fixture.CreateInRange<int>(0, collection.Length - 1);
            return collection[elementIndex];
        }
    }
}
