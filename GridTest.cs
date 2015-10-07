using System;
using System.Collections.Generic;

namespace GameOfLife
{
    using NUnit.Framework;

    [TestFixture]
    public class GridTest
    {
        private List<Tuple<int, int>> liveCells = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(3, 3),
            new Tuple<int, int>(3, 4),
            new Tuple<int, int>(4, 4),
        };

        [TestCase(5, 7, Result = 35)]
        [TestCase(1, 1, Result = 1)]
        [TestCase(12, 12, Result = 144)]
        public int TestGenerateGrid(int width, int height)
        {
            Grid grid = new Grid(width, height);

            return grid.Map.GetLength(0) * grid.Map[0].GetLength(0);
        }

        [TestCase(16, "../../smallexploder.txt", "../../smallexploderfinal.txt")]
        [TestCase(3, "../../corner.txt", "../../cornerfinal.txt")]
        public void TestNextGeneration(int numberOfGenerations, string startFile, string endFile)
        {
            Grid grid = new Grid(startFile);
            Grid gridFinal = new Grid(endFile);

            for (int i = 0; i < numberOfGenerations; i++)
            {
                grid.NextGeneration();
            }

            Assert.AreEqual(gridFinal.ToString(), grid.ToString());
        }
    }
}
