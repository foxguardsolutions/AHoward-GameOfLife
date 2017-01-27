using System.Collections.Generic;

namespace GameOfLife
{
    public class Game : IGame
    {
        private IRuleFactory _ruleFactory;
        private IGridFactory _gridFactory;
        private IConsoleWriter _consoleWriter;
        private IRuleset _rules;
        private IGrid _grid;
        private List<LifeState[,]> _generations;

        public const string GRID_NOT_LOADED = "A grid must be loaded before any steps can be taken.";
        public const string INCOMPLETE_RULES = "Cannot step until rules have been defined for all possible cell states.";

        public Game(IRuleFactory ruleFactory, IGridFactory gridFactory, IConsoleWriter consoleWriter, IRuleset rules)
        {
            _ruleFactory = ruleFactory;
            _gridFactory = gridFactory;
            _consoleWriter = consoleWriter;
            _rules = rules;
            _generations = new List<LifeState[,]>();
        }

        public LifeState[,] GetCurrentPattern()
        {
            return _generations[_generations.Count - 1];
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
                _consoleWriter.WriteLine(GRID_NOT_LOADED);
                return false;
            }

            if (!_rules.IsComplete())
            {
                _consoleWriter.WriteLine(INCOMPLETE_RULES);
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

        public void Start()
        {
            Load(DefaultSettings.Seed, true, true);
            SetRuleFor(LifeState.Alive, 2, 3);
            SetRuleFor(LifeState.Dead, 3);
        }
    }
}
