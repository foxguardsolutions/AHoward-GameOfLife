using System;
using System.Collections.Generic;
using System.Linq;
using GameOfLife;

namespace GameOfLifeTests
{
    public static class MockObjects
    {
        public static IRuleset RulesThatLeaveAllCellsDeadExceptIsolatedLiveCells
        {
            get
            {
                var rules = new Ruleset(new RuleFactory());
                rules.SetRuleFor(LifeState.Alive, 0);
                rules.SetRuleFor(LifeState.Dead);
                return rules;
            }
        }

        public static IGrid GridWithAllDeadCellsExceptTwoLiveCellsNeighboringEachOther
        {
            get
            {
                var grid = new SquareTileGrid(MakeDeadCells(3, 4), false, false);
                foreach (var position in TwoPositionsNeighboringEachOther())
                    SetToAlive(grid.GetCellAt(position));

                return grid;
            }
        }

        private static IEnumerable<CellPosition> TwoPositionsNeighboringEachOther()
        {
            yield return new CellPosition(1, 1);
            yield return new CellPosition(1, 2);
        }

        public static IEnumerable<LifeState> PatternWithAllDeadCells
        {
            get { return GridWithAllDeadCells.GetCurrentPattern(); }
        }

        public static IGrid GridWithAllDeadCells
        {
            get { return new SquareTileGrid(MakeDeadCells(3, 4), false, false); }
        }

        public static IGrid GridWithGliderPattern
        {
            get
            {
                var grid = new SquareTileGrid(MakeDeadCells(5, 10), true, true);
                foreach (var position in GliderPatternLiveCellPositions())
                    SetToAlive(grid.GetCellAt(position));

                return grid;
            }
        }

        public static string RepresentationOfGridWithGliderPattern
        {
            get
            {
                var representation =
                    "----*-----{0}" +
                    "--*-*-----{0}" +
                    "---**-----{0}" +
                    "----------{0}" +
                    "----------{0}";
                return string.Format(representation, Environment.NewLine);
            }
        }

        private static IEnumerable<CellPosition> GliderPatternLiveCellPositions()
        {
            yield return new CellPosition(0, 4);
            yield return new CellPosition(1, 2);
            yield return new CellPosition(1, 4);
            yield return new CellPosition(2, 3);
            yield return new CellPosition(2, 4);
        }

        private static void SetToAlive(Cell cell)
        {
            cell.SetNextState(LifeState.Alive);
            cell.AdvanceState();
        }

        private static Cell[][] MakeDeadCells(uint numberOfRows, uint numberOfColumns)
        {
            var cells = new Cell[numberOfRows][];
            for (int row = 0; row < numberOfRows; row++)
                cells[row] = MakeDeadCells(numberOfColumns).ToArray();

            return cells;
        }

        private static IEnumerable<Cell> MakeDeadCells(uint numberOfCells)
        {
            for (int numberAlreadyMade = 0; numberAlreadyMade < numberOfCells; numberAlreadyMade++)
                yield return new Cell(LifeState.Dead);
        }
    }
}
