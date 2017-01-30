using System;
using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class GridFactoryTests : BaseTests
    {
        private LifeState[,] _seed;
        private bool _wrapsOnRows;
        private bool _wrapsOnColumns;
        private GridFactory _gridFactory;

        [SetUp]
        public void SetUp()
        {
            _seed = Fixture.Create<LifeState[,]>();
            _wrapsOnRows = Fixture.Create<bool>();
            _wrapsOnColumns = Fixture.Create<bool>();
            _gridFactory = new GridFactory();
        }

        [Test]
        public void CreateSquareTileGrid_GivenNoWrappingRules_ReturnsNewSquareTileGrid()
        {
            var grid = _gridFactory.CreateSquareTileGrid(_seed);

            Assert.That(grid, Is.TypeOf(typeof(SquareTileGrid)));
        }

        [Test]
        public void CreateSquareTileGrid_GivenWrappingRules_ReturnsNewSquareTileGrid()
        {
            var grid = _gridFactory.CreateSquareTileGrid(_seed, _wrapsOnRows, _wrapsOnColumns);

            Assert.That(grid, Is.TypeOf(typeof(SquareTileGrid)));
        }

        [Test]
        public void CreateHexTileGrid_GivenNoWrappingRules_ReturnsNewHexTileGrid()
        {
            var grid = _gridFactory.CreateHexTileGrid(_seed);

            Assert.That(grid, Is.TypeOf(typeof(ToroidFriendlyHexTileGrid)));
        }

        [Test]
        public void CreateHexTileGrid_GivenWrappingRules_ReturnsNewHexTileGrid()
        {
            var grid = _gridFactory.CreateToroidFriendlyHexTileGrid(_seed, _wrapsOnRows, _wrapsOnColumns);

            Assert.That(grid, Is.TypeOf(typeof(ToroidFriendlyHexTileGrid)));
        }

        [Test]
        public void CreateSquareTileGrid_ReturnsGridWithCellsThatHaveInitialStateMatchingSeedValues()
        {
            var grid = _gridFactory.CreateSquareTileGrid(_seed, _wrapsOnRows, _wrapsOnColumns);

            AssertGridCellsHaveInitialStateMatchingSeedValues(grid, _seed);
        }

        [Test]
        public void CreateDefaultGrid_ReturnsGridWithCellsThatHaveInitialStateMatchingDefaultSeedValues()
        {
            var grid = _gridFactory.CreateDefaultGrid();

            AssertGridCellsHaveInitialStateMatchingSeedValues(grid, DefaultSettings.Seed);
        }

        [Test]
        public void CreateToroidFriendlyHexTileGrid_GivenSeedWithEvenNumberOfRows_ReturnsGridWithCellsThatHaveInitialStateMatchingSeedValues()
        {
            var seedWithEvenNumberOfRows = AddRowsOfCellsUntilNumberOfRowsIsEven(_seed, Fixture.Create<LifeState>());
            var grid = _gridFactory.CreateToroidFriendlyHexTileGrid(seedWithEvenNumberOfRows, _wrapsOnRows, _wrapsOnColumns);

            AssertGridCellsHaveInitialStateMatchingSeedValues(grid, seedWithEvenNumberOfRows);
        }

        [Test]
        public void CreateToroidFriendlyHexTileGrid_GivenSeedWithOddNumberOfRows_ReturnsGridWithCellsThatHaveInitialStateMatchingSeedValuesPlusAnExtraRowOfDeadCells()
        {
            var seedWithOddNumberOfRows = AddRowsOfCellsUntilNumberOfRowsIsOdd(_seed, Fixture.Create<LifeState>());
            var grid = _gridFactory.CreateToroidFriendlyHexTileGrid(seedWithOddNumberOfRows, _wrapsOnRows, _wrapsOnColumns);

            var modifiedSeed = AddRowsOfCellsUntilNumberOfRowsIsEven(_seed, LifeState.Dead);

            AssertGridCellsHaveInitialStateMatchingSeedValues(grid, modifiedSeed);
        }

        private LifeState[,] AddRowsOfCellsUntilNumberOfRowsIsEven(LifeState[,] pattern, LifeState stateOfCellsInAddedRows)
        {
            var additionalRowsToCreate = pattern.GetLength(0) % 2;

            return AddRowsOfDeadCells(pattern, additionalRowsToCreate, stateOfCellsInAddedRows);
        }

        private LifeState[,] AddRowsOfCellsUntilNumberOfRowsIsOdd(LifeState[,] pattern, LifeState stateOfCellsInAddedRows)
        {
            var additionalRowsToCreate = (pattern.GetLength(0) + 1) % 2;

            return AddRowsOfDeadCells(pattern, additionalRowsToCreate, stateOfCellsInAddedRows);
        }

        private LifeState[,] AddRowsOfDeadCells(LifeState[,] pattern, int additionalRowsToCreate, LifeState stateOfCellsInAddedRows)
        {
            var rowsInPattern = pattern.GetLength(0);
            var columnsInPattern = pattern.GetLength(1);
            var newPattern = new LifeState[rowsInPattern + additionalRowsToCreate, columnsInPattern];

            for (int row = 0; row < rowsInPattern; row++)
            {
                for (int column = 0; column < columnsInPattern; column++)
                    newPattern[row, column] = pattern[row, column];
            }

            return newPattern;
        }

        private void AssertGridCellsHaveInitialStateMatchingSeedValues(IGrid grid, LifeState[,] seed)
        {
            foreach (var position in grid)
            {
                var cell = grid.GetCellAt(position);
                var seedValue = seed[position.DimensionOne, position.DimensionTwo];
                Assert.That(cell.CurrentState, Is.EqualTo(seedValue));
            }
        }
    }
}
