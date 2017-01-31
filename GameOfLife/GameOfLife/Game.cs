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
        private IConsole _console;
        private IRuleset _rules;
        private IGrid _grid;
        private List<IEnumerable<IEnumerable<LifeState>>> _generations;

        public Game(IRuleFactory ruleFactory, IGridFactory gridFactory, IConsole console, IRuleset rules)
        {
            _ruleFactory = ruleFactory;
            _gridFactory = gridFactory;
            _console = console;
            _rules = rules;
            _generations = new List<IEnumerable<IEnumerable<LifeState>>>();
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
            return _generations[_generations.Count - 1];
        }

        public void Start()
        {
            Load(DefaultSettings.Seed, true, true);
            SetRuleFor(LifeState.Alive, 2, 3);
            SetRuleFor(LifeState.Dead, 3);
        }

        public void Load(LifeState[,] seed)
        {
            Load(seed, false, false);
        }

        public void Load(LifeState[,] seed, bool wrapsOnRows, bool wrapsOnColumns)
        {
            _grid = _gridFactory.CreateSquareTileGrid(seed, wrapsOnRows, wrapsOnColumns);
            _generations.Add(_grid.GetCurrentPattern());
        }

        public void SetRuleFor(LifeState state, params uint[] neighborCountsYieldingLive)
        {
            var newRule = _ruleFactory.Create(neighborCountsYieldingLive);
            _rules[state] = newRule;
        }

        public void Step()
        {
            if (IsReadyToStep())
            {
                ApplyRulesToGrid();
                AdvanceGridState();
                _generations.Add(_grid.GetCurrentPattern());
            }
        }

        private bool IsReadyToStep()
        {
            if (_grid == null)
            {
                _console.WriteLine(GRID_NOT_LOADED);
                return false;
            }

            if (!_rules.IsComplete())
            {
                _console.WriteLine(INCOMPLETE_RULES);
                return false;
            }

            return true;
        }

        private void ApplyRulesToGrid()
        {
            foreach (var cellPosition in _grid)
            {
                ApplyRulesToCellAt(cellPosition);
            }
        }

        private void ApplyRulesToCellAt(CellPosition position)
        {
            var cell = _grid.GetCellAt(position);
            var neighbors = _grid.GetNeighborsOfCellAt(position);
            _rules.SetNextState(cell, neighbors);
        }

        private void AdvanceGridState()
        {
            foreach (var cellPosition in _grid)
            {
                AdvanceStateOfCellAt(cellPosition);
            }
        }

        private void AdvanceStateOfCellAt(CellPosition position)
        {
            var cell = _grid.GetCellAt(position);
            cell.AdvanceState();
        }

        public uint CountGenerations()
        {
            return (uint)_generations.Count;
        }
    }
}
