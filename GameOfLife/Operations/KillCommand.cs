namespace GameOfLife.Operations
{
    public class KillCommand : ICommand
    {
        public CellOperation Operation => CellOperation.Kill;

        public void Execute(Cell cell)
        {
            cell.Alive = false;
        }
    }
}
