namespace GameOfLife.Operations
{
    public interface ICommand
    {
        CellOperation Operation { get; }
        void Execute(Cell cell);
    }
}
