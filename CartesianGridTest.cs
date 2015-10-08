using System;
using System.IO;

namespace GameOfLife
{
    using NUnit.Framework;

    [TestFixture]
    public class CartesianGridTest
    {
        [TestCase(5, 7, Result = 35)]
        [TestCase(1, 1, Result = 1)]
        [TestCase(12, 12, Result = 144)]
        public int TestGenerateGrid(int width, int height)
        {
            CartesianGrid cartesianGrid = new CartesianGrid(width, height);

            return cartesianGrid.Map.GetLength(0) * cartesianGrid.Map[0].GetLength(0);
        }

        [TestCase(0, 0, Result = 0)]
        [TestCase(11, 8, Result = 1)]
        [TestCase(11, 9, Result = 3)]
        [TestCase(11, 10, Result = 5)]
        [TestCase(11, 12, Result = 2)]
        [TestCase(20, 20, Result = 0)]
        public int TestNumberOfLiveNeighbors(int x, int y)
        {
            string[] lines = File.ReadAllLines("../../smallexploder.txt");

            CartesianGrid cartesianGrid = new CartesianGrid(lines);

            CartesianGridLogic cartesianGridLogic = new CartesianGridLogic(cartesianGrid);

            return cartesianGridLogic.NumberOfLiveNeighbors(cartesianGrid.Map[x][y]);
        }

        [TestCase(16, "../../smallexploder.txt", "../../smallexploderfinal.txt")]
        [TestCase(3, "../../corner.txt", "../../cornerfinal.txt")]
        public void TestNextGeneration(int numberOfGenerations, string startFile, string endFile)
        {
            string[] startLines = File.ReadAllLines(startFile);
            string[] endLines = File.ReadAllLines(endFile);

            CartesianGrid cartesianGrid = new CartesianGrid(startLines);
            CartesianGrid cartesianGridFinal = new CartesianGrid(endLines);

            for (int i = 0; i < numberOfGenerations; i++)
            {
                cartesianGrid.NextGeneration();

                Console.WriteLine(cartesianGrid.ToString());
            }

            Assert.AreEqual(cartesianGridFinal.ToString(), cartesianGrid.ToString());
        }
    }
}
