namespace GameOfLife
{
    public class Cell
    {
        internal const string AliveCellString = "*";
        internal const string DeadCellString = ".";

        public bool Alive { get; set; }

        public Cell(bool alive)
        {
            Alive = alive;
        }

        public override string ToString() => Alive ? AliveCellString : DeadCellString;

        public static bool TryParse(string s, out Cell result)
        {
            if (s == AliveCellString)
                result = new Cell(true);
            else if (s == DeadCellString)
                result = new Cell(false);
            else
                result = null;

            return result != null;
        }
    }
}
