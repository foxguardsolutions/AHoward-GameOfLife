namespace GameOfLife.Operations
{
    public class BringToLifeCommand : ICommand
    {
        public CellOperation Operation => CellOperation.BringToLife;

        public void Execute(Cell cell)
        {
            cell.Alive = true;
        }
    }
}
