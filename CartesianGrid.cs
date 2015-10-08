using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class CartesianGrid : IGrid
    {
        public int Height { get; }
        public int Width { get; }
        public CartesianGridCell[][] Map { get; set; }
        public CartesianGridCellLogic CellLogic { get; }

        public CartesianGrid(int width, int height)
        {
            Width = width;
            Height = height;
            CellLogic = new CartesianGridCellLogic();

            GenerateGrid();
        }

        public CartesianGrid(string[] lines)
        {
            Width = lines[0].Length;
            Height = lines.Length;
            CellLogic = new CartesianGridCellLogic();

            GenerateGrid();

            AddLiveCells(lines);
        }

        private void GenerateGrid()
        {
            Map = Enumerable.Range(0, Width).Select((row) => Enumerable.Range(0, Height).Select((col) => new CartesianGridCell(row, col)).ToArray()).ToArray();
        }

        private void AddLiveCells(IList<string> lines)
        {
            for (int h = 0; h < lines.Count; h++)
            {
                for (int w = 0; w < lines[h].Length; w++)
                {
                    if (lines[h][w].Equals('*'))
                    {
                        Map[w][h].IsAlive = true;
                    }
                }
            }
        }

        public void NextGeneration()
        {
            CartesianGridCell[][] emptyMap = new CartesianGrid(Width, Height).Map;

            CartesianGridLogic cartesianGridLogic = new CartesianGridLogic(this);

            for (int w = 0; w < Width; w++)
            {
                for (int h = 0; h < Height; h++)
                {
                    emptyMap[w][h].IsAlive = CellLogic.DetermineIfCellLives(Map[w][h], cartesianGridLogic.NumberOfLiveNeighbors(Map[w][h]));
                }
            }

            Map = emptyMap;
        }

        public override string ToString()
        {
            string grid = null;

            for (int h = 0; h < Map[1].Length; h++)
            {
                for (int w = 0; w < Map[0].Length; w++)
                {
                    CartesianGridCell currentCell = Map[w][h];

                    grid += currentCell.IsAlive ? "*" : ".";
                }

                grid += "\n";
            }

            return grid;
        }
    }
}
