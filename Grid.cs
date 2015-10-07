using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameOfLife
{
    public class Grid
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        public GridCell[][] Map { get; set; }

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;

            GenerateGrid();
        }

        public Grid(string file)
        {
            string[] lines = File.ReadAllLines(file);

            Width = lines[0].Length;
            Height = lines.Length;

            GenerateGrid();

            AddLiveCells(lines);
        }

        private void GenerateGrid()
        {
            Map = Enumerable.Range(0, Width).Select((row) => Enumerable.Range(0, Height).Select((col) => new GridCell(row, col)).ToArray()).ToArray();

            SetNeighborsForCell();
        }

        private void SetNeighborsForCell()
        {
            for (int w = 0; w < Width; w++)
            {
                for (int h = 0; h < Height; h++)
                {
                    GridCell cell = Map[w][h];

                    cell.CellToTheNorth = (cell.Y - 1) >= 0 ? Map[cell.X][cell.Y - 1] : null;
                    cell.CellToTheEast = (cell.X + 1) < Width ? Map[cell.X + 1][cell.Y] : null;
                    cell.CellToTheSouth = (cell.Y + 1) < Height ? Map[cell.X][cell.Y + 1] : null;
                    cell.CellToTheWest = (cell.X - 1) >= 0 ? Map[cell.X - 1][cell.Y] : null;
                    cell.CellToTheNorthEast = (cell.Y - 1) >= 0 && (cell.X + 1) < Width ? Map[cell.X + 1][cell.Y - 1] : null;
                    cell.CellToTheNorthWest = (cell.Y - 1) >= 0 && (cell.X - 1) >= 0 ? Map[cell.X - 1][cell.Y - 1] : null;
                    cell.CellToTheSouthEast = (cell.Y + 1) < Height && (cell.X + 1) < Width ? Map[cell.X + 1][cell.Y + 1] : null;
                    cell.CellToTheSouthWest = (cell.Y + 1) < Height && (cell.X - 1) >= 0 ? Map[cell.X - 1][cell.Y + 1] : null;
                }
            }
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
            GridCell[][] emptyMap = new Grid(Width, Height).Map;

            for (int w = 0; w < Width; w++)
            {
                for (int h = 0; h < Height; h++)
                {
                    emptyMap[w][h].IsAlive = DetermineIfCellLives(Map[w][h]);
                }
            }

            Map = emptyMap;
        }

        private bool DetermineIfCellLives(GridCell cell)
        {
            int liveNeighbors = cell.NumberOfLiveNeighbors();

            return cell.IsAlive ? DoesLiveCellLive(liveNeighbors) : DoesDeadCellComeAlive(liveNeighbors);
        }

        private bool DoesLiveCellLive(int liveNeighbors)
        {
            return liveNeighbors >= 2 && liveNeighbors <= 3;
        }

        private bool DoesDeadCellComeAlive(int liveNeighbors)
        {
            return liveNeighbors == 3;
        }

        public override string ToString()
        {
            string grid = null;

            for (int h = 0; h < Map[1].Length; h++)
            {
                for (int w = 0; w < Map[0].Length; w++)
                {
                    GridCell currentCell = Map[w][h];

                    grid += currentCell.IsAlive ? "*" : ".";
                }

                grid += "\n";
            }

            return grid;
        }
    }
}
