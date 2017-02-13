using System.Collections.Generic;
using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class HexTileGridGetNeighborsOnMultiRowMultiColumnGridTests : GridGetNeighborsOnMultiRowMultiColumnGridTests
    {
        [Test]
        public void GetNeighbors_GivenCellPositionInAcuteCornerOfGridWithNoWrapping_YieldsTwoNeighbors()
        {
            var grid = GivenHexTileGridWithNoWrapping();
            var acuteCornerPosition = GivenRowEndPositionOffsetTowardExteriorOfGrid(RowOnOneEdgeOfGrid);
            var cornerCell = grid.GetCellAt(acuteCornerPosition);
            var neighborsOfCornerCell = grid.GetNeighborsOfCellAt(acuteCornerPosition);

            Assert.That(neighborsOfCornerCell, Has.Exactly(2).Items);
        }

        [Test]
        public void GetNeighbors_GivenCellPositionInObtuseCornerOfGridWithNoWrapping_YieldsThreeNeighbors()
        {
            var grid = GivenHexTileGridWithNoWrapping();
            var obtuseCornerPosition = GivenRowEndPositionOffsetTowardInteriorOfGrid(RowOnOneEdgeOfGrid);
            var cornerCell = grid.GetCellAt(obtuseCornerPosition);
            var neighborsOfCornerCell = grid.GetNeighborsOfCellAt(obtuseCornerPosition);

            Assert.That(neighborsOfCornerCell, Has.Exactly(3).Items);
        }

        [Test]
        public void GetNeighbors_GivenCellPositionOnEndOfNonEdgeRowOffsetTowardExteriorOfGridWithNoColumnWrapping_YieldsThreeNeighbors()
        {
            var grid = GivenHexTileGridWithNoWrapping();
            var exteriorOffsetEdgePosition = GivenRowEndPositionOffsetTowardExteriorOfGrid(RowNotOnEdgeOfGrid);
            var edgeCell = grid.GetCellAt(exteriorOffsetEdgePosition);
            var neighborsOfEdgeCell = grid.GetNeighborsOfCellAt(exteriorOffsetEdgePosition);

            Assert.That(neighborsOfEdgeCell, Has.Exactly(3).Items);
        }

        [Test]
        public void GetNeighbors_GivenCellPositionOnEndOfNonEdgeRowMoreInteriorThanItsNeighborRowsWithNoColumnWrapping_YieldsFiveNeighbors()
        {
            var grid = GivenHexTileGridWithNoWrapping();
            var interiorOffsetEdgePosition = GivenRowEndPositionOffsetTowardInteriorOfGrid(RowNotOnEdgeOfGrid);
            var edgeCell = grid.GetCellAt(interiorOffsetEdgePosition);
            var neighborsOfEdgeCell = grid.GetNeighborsOfCellAt(interiorOffsetEdgePosition);

            Assert.That(neighborsOfEdgeCell, Has.Exactly(5).Items);
        }

        [Test]
        public void GetNeighbors_GivenCellInNonEndPositionOnEdgeRowWithNoRowWrapping_YieldsFourNeighbors()
        {
            var grid = GivenHexTileGridWithNoWrapping();
            var nonEndPositionOnEdgeRow = GivenNonCornerPositionOnEdgeRow();
            var edgeCell = grid.GetCellAt(nonEndPositionOnEdgeRow);
            var neighborsOfEdgeCell = grid.GetNeighborsOfCellAt(nonEndPositionOnEdgeRow);

            Assert.That(neighborsOfEdgeCell, Has.Exactly(4).Items);
        }

        [Test]
        public void GetNeighbors_GivenCellPositionNotOnAGridEdge_YieldsTheSixNeighborsSurroundingTheCell()
        {
            var grid = GivenHexTileGrid();
            var interiorPosition = GivenInteriorPosition();
            var interiorCell = grid.GetCellAt(interiorPosition);
            var expectedNeighbors = GetExpectedNeighborsOf(interiorPosition, grid);

            var neighborsOfInteriorCell = grid.GetNeighborsOfCellAt(interiorPosition);

            Assert.That(neighborsOfInteriorCell, Is.EquivalentTo(expectedNeighbors));
        }

        private IEnumerable<Cell> GetExpectedNeighborsOf(CellPosition interiorPosition, IGrid grid)
        {
            var neighborPositions = GetNeighborPositionsOf(interiorPosition);

            foreach (var position in neighborPositions)
                yield return grid.GetCellAt(position);
        }

        private IEnumerable<CellPosition> GetNeighborPositionsOf(CellPosition interiorPosition)
        {
            var centerRow = interiorPosition.DimensionOne;
            var centerColumn = interiorPosition.DimensionTwo;

            yield return new CellPosition(centerRow, centerColumn - 1);
            yield return new CellPosition(centerRow, centerColumn + 1);
            yield return new CellPosition(centerRow - 1, centerColumn);
            yield return new CellPosition(centerRow + 1, centerColumn);

            var centerRowIsOffsetHigh = centerRow % 2 == 1;
            if (centerRowIsOffsetHigh)
            {
                yield return new CellPosition(centerRow - 1, centerColumn + 1);
                yield return new CellPosition(centerRow + 1, centerColumn + 1);
            }
            else
            {
                yield return new CellPosition(centerRow - 1, centerColumn - 1);
                yield return new CellPosition(centerRow + 1, centerColumn - 1);
            }
        }

        private IGrid GivenHexTileGrid()
        {
            return Fixture.Create<HexTileGrid>();
        }

        private IGrid GivenHexTileGridWithNoWrapping()
        {
            Fixture.Register(() => false);
            return Fixture.Create<HexTileGrid>();
        }

        private CellPosition GivenRowEndPositionOffsetTowardExteriorOfGrid(uint rowNumber)
        {
            var rowNumberIsEven = rowNumber % 2 == 0;
            if (rowNumberIsEven)
                return FirstPositionInRow(rowNumber);

            return LastPositionInRow(rowNumber);
        }

        private CellPosition GivenRowEndPositionOffsetTowardInteriorOfGrid(uint rowNumber)
        {
            var rowNumberIsEven = rowNumber % 2 == 0;
            if (rowNumberIsEven)
                return LastPositionInRow(rowNumber);

            return FirstPositionInRow(rowNumber);
        }

        private CellPosition FirstPositionInRow(uint rowNumber)
        {
            var firstColumnNumber = 0;
            return new CellPosition(rowNumber, (uint)firstColumnNumber);
        }

        private CellPosition LastPositionInRow(uint rowNumber)
        {
            var lastColumnNumber = Cells[0].Length - 1;
            return new CellPosition(rowNumber, (uint)lastColumnNumber);
        }
    }
}
