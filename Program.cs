using System;

namespace GameOfLife
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Grid grid = new Grid(args[0]);

            Console.WriteLine("Starting grid, press \"Y\" to view next generation");
            Console.WriteLine(grid.ToString());

            while (Console.ReadLine() == "y")
            {
                grid.NextGeneration();

                Console.WriteLine(grid.ToString());
                Console.WriteLine("Press \"Y\" to view next generation");
            }
        }
    }
}
