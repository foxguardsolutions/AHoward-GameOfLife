using GameOfLife;

namespace GameOfLifeConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var consoleWriter = new ConsoleWriter();
            var game = new Game(new RuleFactory(), new GridFactory(), consoleWriter, new Ruleset());
            var consoleController = new ConsoleInterface(consoleWriter, game, new TextCommandParser(), new CommandRunner());
            consoleController.Start();
        }
    }
}
