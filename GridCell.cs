using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class GridCell
    {
        public int X { get; }
        public int Y { get; }
        public bool IsAlive { get; set; }
        public GridCell CellToTheNorth { get; set; }
        public GridCell CellToTheEast { get; set; }
        public GridCell CellToTheSouth { get; set; }
        public GridCell CellToTheWest { get; set; }
        public GridCell CellToTheNorthEast { get; set; }
        public GridCell CellToTheNorthWest { get; set; }
        public GridCell CellToTheSouthEast { get; set; }
        public GridCell CellToTheSouthWest { get; set; }

        public GridCell(int x, int y, bool isAlive = false)
        {
            X = x;
            Y = y;
            IsAlive = isAlive;
        }

        public int NumberOfLiveNeighbors()
        {
            return NeighborsList().Count(i => (i != null && i.IsAlive));
        }

        private IEnumerable<GridCell> NeighborsList()
        {
            List<GridCell> neighborCells = new List<GridCell>
            {
                CellToTheNorth,
                CellToTheEast,
                CellToTheSouth,
                CellToTheWest,
                CellToTheNorthEast,
                CellToTheNorthWest,
                CellToTheSouthEast,
                CellToTheSouthWest
            };

            return neighborCells;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GridCell);
        }

        public bool Equals(GridCell cell)
        {
            return X == cell.X && Y == cell.Y && IsAlive == cell.IsAlive;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }
    }
}
