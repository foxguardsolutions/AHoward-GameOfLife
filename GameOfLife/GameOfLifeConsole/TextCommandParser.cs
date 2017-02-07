using GameOfLife;

namespace GameOfLifeConsole
{
    public class TextCommandParser
    {
        public Command ParseCommand()
        {
            return ParseCommand("d");
        }

        public virtual Command ParseCommand(string input)
        {
            if (input.ToLower().StartsWith("q"))
                return Command.Quit;
            if (input.ToLower().StartsWith("d"))
                return Command.Display;
            if (input.ToLower().StartsWith("r"))
                return Command.Reload;
            return Command.Step;
        }
    }
}
