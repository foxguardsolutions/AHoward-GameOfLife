using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public static class AutoFixtureExtension
    {
        public static T[][] CreateRectangularJaggedArray<T>(this Fixture fixture)
        {
            var numberOfRows = fixture.Create<int>();
            var numberOfColumns = fixture.Create<int>();

            return fixture.CreateRectangularJaggedArray<T>(numberOfRows, numberOfColumns);
        }

        public static T[][] CreateRectangularJaggedArray<T>(this Fixture fixture, int numberOfRows, int numberOfColumns)
        {
            var array = new T[numberOfRows][];

            for (int rowNumber = 0; rowNumber < numberOfRows; rowNumber++)
            {
                array[rowNumber] = new T[numberOfColumns];
                for (int columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
                    array[rowNumber][columnNumber] = fixture.Create<T>();
            }

            return array;
        }

        public static T CreateInRange<T>(this Fixture fixture, int lowerLimit, int upperLimit)
        {
            fixture.Customizations.Add(new RandomNumericSequenceGenerator(lowerLimit, upperLimit));
            var value = fixture.Create<T>();
            fixture.Customizations.RemoveAt(fixture.Customizations.Count - 1);
            return value;
        }

        public static T CreateUnequalToDefault<T>(this Fixture fixture)
        {
            var excludeItem = default(T);
            T newItem;

            do
                newItem = fixture.Create<T>();
            while (newItem.Equals(excludeItem));

            return newItem;
        }

        public static T PickFromValues<T>(this Fixture fixture, params T[] collection)
        {
            var elementIndex = fixture.CreateInRange<int>(0, collection.Length - 1);
            return collection[elementIndex];
        }
    }
}
