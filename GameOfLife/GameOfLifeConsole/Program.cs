using GameOfLife;

namespace GameOfLifeConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var consoleReaderWriter = new ConsoleReaderWriter();
            var commandRunner = new CommandRunner(
                new Ruleset(new RuleFactory()),
                new GridFactory(),
                new GameAdvancer(consoleReaderWriter),
                new GridWriter(consoleReaderWriter));

            var game = new ConsoleGame(consoleReaderWriter, new TextCommandParser(), commandRunner);
            game.Start();
        }
    }
}
