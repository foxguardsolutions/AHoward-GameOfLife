using GameOfLife.Environments;
using GameOfLife.Operations;
using System.Collections.Generic;
using System.Linq;
using GameOfLife.EnvironmentRules;

namespace GameOfLife
{
    public class Game
    {
        private readonly IEnvironmentRules _rules;
        private readonly List<CellEnvironment> _environments;
        
        public int Generation { get; private set; }
        public Grid Grid { get; }

        public Game(int generation, Grid grid, IEnvironmentRules rules, CellEnvironmentFactory factory)
        {
            Generation = generation;
            Grid = grid;
            _rules = rules;
            _environments = factory.GetEnvironments(Grid).ToList();
        }

        public void Proceed()
        {
            var operations = _environments.Select(e => _rules.GetOperationToPerformOnCell(e)).ToList();

            for (var i = 0; i < _environments.Count; i++)
            {
                var command = CommandFactory.GetCommand(operations[i]);
                command.Execute(_environments[i].Cell);
            }

            Generation++;
        }

        public override string ToString() => $"Generation {Generation}:{GridFactory.NewLine}{Grid}";
    }
}
