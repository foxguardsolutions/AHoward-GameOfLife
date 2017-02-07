namespace GameOfLife
{
    public interface ICommandRunner
    {
        void Execute(Command command);
    }
}