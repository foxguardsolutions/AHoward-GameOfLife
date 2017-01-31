namespace GameOfLife
{
    public class Cell
    {
        public LifeState CurrentState { get; private set; }
        private LifeState _nextState;

        public Cell(LifeState initialState)
        {
            CurrentState = initialState;
        }

        public void SetNextState(LifeState newState)
        {
            _nextState = newState;
        }

        public void AdvanceState()
        {
            CurrentState = _nextState;
        }
    }
}
