using System.Collections.Generic;
using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class SquareTileGridIterationTests : SquareTileGridTests
    {
        [Test]
        public void IterationOverGrid_YieldsPositionsMatchingAllPossibleSeedIndices()
        {
            var seed = Fixture.Create<LifeState[,]>();
            var allPossibleSeedIndices = GetCartesianProductOfPositionsIn(seed);
            var grid = GridFactory.CreateSquareTileGrid(seed);
            var resultOfIteration = IterateOver(grid);

            Assert.That(resultOfIteration, Is.EquivalentTo(allPossibleSeedIndices));
        }

        private IEnumerable<CellPosition> IterateOver(SquareTileGrid grid)
        {
            foreach (var position in grid)
                yield return position;
        }

        private IEnumerable<CellPosition> GetCartesianProductOfPositionsIn<T>(T[,] seed)
        {
            for (uint i = 0; i < seed.GetLength(0); i++)
            {
                for (uint j = 0; j < seed.GetLength(1); j++)
                    yield return new CellPosition(i, j);
            }
        }
    }
}
