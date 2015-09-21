using System;
using System.IO;

namespace GameOfLife
{
    public class GameOfLife
    {
        public static int Main(string[] args)
        {
            string[] inputLines = File.ReadAllLines(args[0]);
            Plane plane = new Plane();
            for (int row = 0; row < inputLines.Length - 1; row++)
            {
                string line = inputLines[row].Trim('\n');
                int column = 0;
                foreach (var character in line)
                {
                    plane.AddCell(character, column++, row);
                }
            }

            int iterations = 1;
            if (args.Length == 2)
            {
                iterations = int.Parse(args[1]);
            }

            for (int i = 0; i <= iterations; i++)
            {
                Console.WriteLine("{0}\n", plane);
                plane = plane.NextGeneration();
            }

            return 0;
        }
    }
}
