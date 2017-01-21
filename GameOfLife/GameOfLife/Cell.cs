namespace GameOfLife
{
    public class Cell
    {
        public LifeState CurrentState { get; private set; }
        public LifeState NextState { private get; set; }

        public Cell(LifeState initialState)
        {
            CurrentState = initialState;
        }

        public void AdvanceState()
        {
            CurrentState = NextState;
        }
    }
}
