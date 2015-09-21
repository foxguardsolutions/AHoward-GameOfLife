using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GameOfLife
{
    [TestFixture]
    public class PlaneTests
    {
        [Test]
        public void AddingLiveCellIncreasesCellCountAndDeadCellCount()
        {
            Plane plane = new Plane();
            plane.AddCell(new Cell(LifeStates.ALIVE, 0, 0));
            Assert.AreEqual(1, plane.LiveCellCount);
            Assert.AreEqual(8, plane.SignificantDeadCellCount);
        }

        [Test]
        public void AddingLiveCellIncreasesCellCountAndDeadCellCountProperly()
        {
            Plane plane = new Plane();
            plane.AddCell(new Cell(LifeStates.ALIVE, 0, 0));
            plane.AddCell(new Cell(LifeStates.ALIVE, 1, 0));
            Assert.AreEqual(2, plane.LiveCellCount);
            Assert.AreEqual(10, plane.SignificantDeadCellCount);
        }

        [Test]
        public void NextGenerationCorrectlyGeneratesNextGeneration()
        {
            Plane plane = new Plane();
            plane.AddCell(new Cell(LifeStates.ALIVE, 0, 0));
            plane.AddCell(new Cell(LifeStates.ALIVE, 1, 0));
            Plane nextGen = plane.NextGeneration();
            Assert.AreEqual(0, nextGen.LiveCellCount);
            Assert.AreEqual(0, nextGen.SignificantDeadCellCount);
        }

        [Test]
        public void ToStringProperlyGeneratesString()
        {
            Plane plane = new Plane();
            plane.AddCell(new Cell(LifeStates.ALIVE, 0, 0));
            plane.AddCell(new Cell(LifeStates.ALIVE, 1, 0));
            Assert.AreEqual("....\n.**.\n....", plane.ToString());
        }
    }
}
