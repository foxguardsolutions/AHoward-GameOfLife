using System;
using System.IO;

namespace GameOfLife
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(args[0]);

            CartesianGrid cartesianGrid = new CartesianGrid(lines);

            Console.WriteLine("Starting grid, press \"Y\" to view next generation");
            Console.WriteLine(cartesianGrid.ToString());

            while (Console.ReadLine() == "y")
            {
                cartesianGrid.NextGeneration();

                Console.WriteLine(cartesianGrid.ToString());
                Console.WriteLine("Press \"Y\" to view next generation");
            }
        }
    }
}
