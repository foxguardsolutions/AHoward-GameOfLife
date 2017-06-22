namespace GameOfLife
{
    public class Grid
    {
        public readonly Cell[,] Cells;
        public int Height => Cells.GetLength(0);
        public int Width => Cells.GetLength(1);

        public Grid(Cell[,] cells)
        {
            Cells = cells;
        }

        public override string ToString()
        {
            var gridText = $"{Height} {Width}";

            for (var row = 0; row < Height; row++)
            {
                gridText += GridFactory.NewLine;

                for (var column = 0; column < Width; column++)
                    gridText += Cells[row, column];
            }

            return gridText;
        }
    }
}
