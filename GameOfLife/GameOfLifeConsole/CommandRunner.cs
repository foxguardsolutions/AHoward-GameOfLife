using GameOfLife;

namespace GameOfLifeConsole
{
    public class CommandRunner
    {
        public virtual void Execute(Command command, IGame game)
        {
            if (command == Command.Display || command == Command.Quit)
                return;
            else if (command == Command.Reload)
                game.Start();
            else
                game.Step();
        }
    }
}
