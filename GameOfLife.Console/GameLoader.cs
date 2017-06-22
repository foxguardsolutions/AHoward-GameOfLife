using GameOfLife.Console.InputReader;
using GameOfLife.EnvironmentRules;
using GameOfLife.Environments;
using System.Linq;
using System.Text.RegularExpressions;

namespace GameOfLife.Console
{
    public class GameLoader
    {
        private readonly IInputReader _inputReader;

        public GameLoader(IInputReader inputReader)
        {
            _inputReader = inputReader;
        }

        public Game LoadGame(IEnvironmentRules rules, bool wrapEdges)
        {
            var gameText = _inputReader.ReadInput();
            var gameLines = gameText.Split(GridFactory.NewLine[0]);
            var generationNumber = GetStartingGenerationNumber(gameLines[0]);
            var gridString = string.Join(GridFactory.NewLine, gameLines.Skip(1));
            var grid = GridFactory.Parse(gridString);
            var factory = new CellEnvironmentFactory(wrapEdges);
            return new Game(generationNumber, grid, rules, factory);
        }

        private int GetStartingGenerationNumber(string generationHeader)
        {
            var numberText = Regex.Match(generationHeader, @"\d+").Value;
            return int.TryParse(numberText, out int generation) ? generation : 1;
        }
    }
}
