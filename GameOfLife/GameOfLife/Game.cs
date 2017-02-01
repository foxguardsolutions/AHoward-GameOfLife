using System;
using System.Collections.Generic;
using System.IO;

namespace GameOfLife
{
    public class Game : IGame
    {
        private const string GRID_NOT_LOADED = "A grid must be loaded before any steps can be taken.";
        private const string INCOMPLETE_RULES = "Cannot step until rules have been defined for all possible cell states.";

        private IRuleFactory _ruleFactory;
        private IGridFactory _gridFactory;
        private IConsoleReaderWriter _console;

        public IRuleset Rules { get; set; }
        public IGrid Grid { get; set; }
        public IList<IEnumerable<IEnumerable<LifeState>>> Generations { get; set; }

        public Game(IRuleFactory ruleFactory, IGridFactory gridFactory, IConsoleReaderWriter console, IRuleset rules)
        {
            _ruleFactory = ruleFactory;
            _gridFactory = gridFactory;
            _console = console;
            Rules = rules;
            Generations = new List<IEnumerable<IEnumerable<LifeState>>>();
        }

        public void WriteCurrentPatternToConsole()
        {
            WriteCurrentPattern(_console.Out);
        }

        public void WriteCurrentPattern(TextWriter textWriter)
        {
            var currentPattern = GetCurrentPattern();

            foreach (var row in currentPattern)
            {
                foreach (var state in row)
                    textWriter.Write(DefaultSettings.ToString(state));
                textWriter.Write(Environment.NewLine);
            }
        }

        public IEnumerable<IEnumerable<LifeState>> GetCurrentPattern()
        {
            return Generations[Generations.Count - 1];
        }

        public void Step()
        {
            if (IsReadyToStep())
            {
                ApplyRulesToGrid();
                AdvanceGridState();
                Generations.Add(Grid.GetCurrentPattern());
            }
        }

        private bool IsReadyToStep()
        {
            if (Grid == null)
            {
                _console.WriteLine(GRID_NOT_LOADED);
                return false;
            }

            if (!Rules.IsComplete())
            {
                _console.WriteLine(INCOMPLETE_RULES);
                return false;
            }

            return true;
        }

        private void ApplyRulesToGrid()
        {
            foreach (var cellPosition in Grid)
            {
                ApplyRulesToCellAt(cellPosition);
            }
        }

        private void ApplyRulesToCellAt(CellPosition position)
        {
            var cell = Grid.GetCellAt(position);
            var neighbors = Grid.GetNeighborsOfCellAt(position);
            Rules.SetNextState(cell, neighbors);
        }

        private void AdvanceGridState()
        {
            foreach (var cellPosition in Grid)
            {
                AdvanceStateOfCellAt(cellPosition);
            }
        }

        private void AdvanceStateOfCellAt(CellPosition position)
        {
            var cell = Grid.GetCellAt(position);
            cell.AdvanceState();
        }

        public uint CountGenerations()
        {
            return (uint)Generations.Count;
        }
    }
}
