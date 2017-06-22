namespace GameOfLife.Operations
{
    public class NoActionCommand : ICommand
    {
        public CellOperation Operation => CellOperation.NoAction;

        public void Execute(Cell cell) { }
    }
}
