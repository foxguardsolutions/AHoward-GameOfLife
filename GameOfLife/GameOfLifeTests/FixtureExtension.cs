using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public static class FixtureExtension
    {
        public static T CreateUnequalToDefault<T>(this Fixture fixture)
        {
            var excludeItem = default(T);
            T newItem;

            do
                newItem = fixture.Create<T>();
            while (newItem.Equals(excludeItem));

            return newItem;
        }
    }
}
