using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class Plane
    {
        private List<Cell> _liveCells = new List<Cell>();
        private List<Cell> _potentiallySignificantCells = new List<Cell>();

        public int LiveCellCount
        {
            get { return _liveCells.Count; }
        }

        public int SignificantDeadCellCount
        {
            get { return _potentiallySignificantCells.Count; }
        }

        public Plane()
        {
        }

        public void AddCell(Cell cell)
        {
            if (cell.Alive)
            {
                cell.LiveNeighbors = 0;
                _liveCells.Add(cell);
                if (_potentiallySignificantCells.Contains(cell))
                {
                    _potentiallySignificantCells.Remove(cell);
                }

                AddDeadCells(cell);
            }
        }

        public void AddCell(char initValue, int x, int y)
        {
            Cell cell = new Cell(initValue, x, y);
            AddCell(cell);
        }

        private void AddDeadCells(Cell cell)
        {
            for (int x = cell.X - 1; x <= cell.X + 1; x++)
            {
                for (int y = cell.Y - 1; y <= cell.Y + 1; y++)
                {
                    if (cell.X != x || cell.Y != y)
                    {
                        Cell newCell = new Cell(LifeStates.DEAD, x, y);
                        if (!_potentiallySignificantCells.Contains(newCell) && !_liveCells.Contains(newCell))
                        {
                            _potentiallySignificantCells.Add(newCell);
                        }
                    }
                }
            }
        }

        private void CalculateLiveNeighbors()
        {
            foreach (var cellA in _liveCells)
            {
                foreach (var cellB in _liveCells.SkipWhile(x => x != cellA))
                {
                    if (cellA.Neighbors(cellB))
                    {
                        cellA.LiveNeighbors++;
                        cellB.LiveNeighbors++;
                    }
                }

                IncreaseDeadCellNeighborCount(cellA);
            }
        }

        private void IncreaseDeadCellNeighborCount(Cell otherCell)
        {
            foreach (var deadCell in _potentiallySignificantCells)
            {
                if (deadCell.Neighbors(otherCell))
                {
                    deadCell.LiveNeighbors++;
                }
            }
        }

        public Plane NextGeneration()
        {
            CalculateLiveNeighbors();
            Plane nextGen = new Plane();
            foreach (var cell in _liveCells.Where(x => x.SurvivesGeneration()))
            {
                nextGen.AddCell(cell);
            }

            foreach (var cell in _potentiallySignificantCells.Where(x => x.SpawnsInGeneration()))
            {
                nextGen.AddCell(new Cell(LifeStates.ALIVE, cell.X, cell.Y));
            }

            return nextGen;
        }

        public List<Cell> GetAllSignificantCells()
        {
            List<Cell> allCells = _potentiallySignificantCells.Concat(_liveCells).ToList();
            allCells.Sort();
            return allCells;
        }

        public override string ToString()
        {
            var allCells = GetAllSignificantCells();
            if (allCells.Count == 0)
            {
                return string.Empty;
            }

            string stringValue = string.Empty;
            var startRow = allCells.First().Y;
            var rowCount = Math.Abs(allCells.Last().Y - startRow) + 1;
            var startColumn = allCells.Select(x => x.X).Min();
            var endColumn = allCells.Select(x => x.X).Max();
            var columnCount = Math.Abs(endColumn - startColumn) + 1;

            foreach (var rowi in Enumerable.Range(startRow, rowCount))
            {
                foreach (var coli in Enumerable.Range(startColumn, columnCount))
                {
                    stringValue += _liveCells.Contains(new Cell(LifeStates.ALIVE, coli, rowi))
                        ? ((char)LifeStates.ALIVE).ToString()
                        : ((char)LifeStates.DEAD).ToString();
                }

                stringValue += "\n";
            }

            return stringValue.Trim('\n');
        }
    }
}
