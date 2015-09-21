using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace GameOfLife
{
    [TestFixture]
    public class CellTests
    {
        [Test]
        public void CellIsDeadIfInitializedWithDot()
        {
            Cell cell = new Cell(LifeStates.DEAD, 0, 0);
            Assert.IsFalse(cell.Alive);
        }

        [Test]
        public void CellIsAliveIfInitializedWithAsterisk()
        {
            Cell cell = new Cell(LifeStates.ALIVE, 0, 0);
            Assert.IsTrue(cell.Alive);
        }

        [TestCase(0, Result = false)]
        [TestCase(1, Result = false)]
        [TestCase(2, Result = true)]
        [TestCase(3, Result = true)]
        [TestCase(4, Result = false)]
        public bool SurvivesGenerationReturnsTrueForOnly2Or3LiveNeighbors(int neighbors)
        {
            Cell cell = new Cell(LifeStates.ALIVE, 0, 0);
            cell.LiveNeighbors = neighbors;
            return cell.SurvivesGeneration();
        }

        [TestCase(0, Result = false)]
        [TestCase(1, Result = false)]
        [TestCase(2, Result = false)]
        [TestCase(3, Result = true)]
        [TestCase(4, Result = false)]
        public bool SpawnsInGenerationReturnsTrueForOnly3LiveNeighbors(int neighbors)
        {
            Cell cell = new Cell(LifeStates.DEAD, 0, 0);
            cell.LiveNeighbors = neighbors;
            return cell.SpawnsInGeneration();
        }

        [TestCase(LifeStates.ALIVE, 0, 0, LifeStates.DEAD, 0, 0, Result = true)]
        [TestCase(LifeStates.ALIVE, 1, 3, LifeStates.DEAD, 1, 3, Result = true)]
        [TestCase(LifeStates.ALIVE, 55, 55, LifeStates.ALIVE, 55, 55, Result = true)]
        [TestCase(LifeStates.DEAD, 10, 10, LifeStates.DEAD, 10, 11, Result = false)]
        public bool EqualsReturnsTrueWhenCoordinatesAreEqual(LifeStates lifeA, int aX, int aY, LifeStates lifeB, int bX, int bY)
        {
            Cell cellA = new Cell(lifeA, aX, aY);
            Cell block4 = new Cell(lifeB, bX, bY);
            return cellA.Equals(block4);
        }

        [TestCase(0, 0, 1, 0, Result = true)]
        [TestCase(0, 0, 0, 1, Result = true)]
        [TestCase(0, 0, 1, 1, Result = true)]
        [TestCase(0, 0, -1, 0, Result = true)]
        [TestCase(0, 0, 0, -1, Result = true)]
        [TestCase(0, 0, -1, -1, Result = true)]
        [TestCase(0, 0, -1, 1, Result = true)]
        [TestCase(0, 0, 1, -1, Result = true)]
        [TestCase(0, 0, 2, 0, Result = false)]
        [TestCase(0, 0, 2, 1, Result = false)]
        [TestCase(0, 0, 1, 2, Result = false)]
        public bool NeighborsReturnsTrueWhenCellsAreNextToEachOther(int aX, int aY, int bX, int bY)
        {
            Cell cellA = new Cell(LifeStates.ALIVE, aX, aY);
            Cell cellB = new Cell(LifeStates.ALIVE, bX, bY);
            return cellA.Neighbors(cellB);
        }

        [TestCase(0, 0, 1, 1, Result = -1)]
        [TestCase(0, 0, 1, 0, Result = -1)]
        [TestCase(0, 0, 0, 1, Result = -1)]
        [TestCase(2, 1, 1, 1, Result = 1)]
        [TestCase(1, 2, 1, 1, Result = 1)]
        [TestCase(0, 0, 0, 0, Result = 0)]
        public int CompareToOrdersCellsByYFirstThenX(int aX, int aY, int bX, int bY)
        {
            Cell cellA = new Cell(LifeStates.ALIVE, aX, aY);
            Cell cellB = new Cell(LifeStates.ALIVE, bX, bY);
            return cellA.CompareTo(cellB);
        }
    }
}
