namespace GameOfLife
{
    public struct CellPosition
    {
        public uint DimensionOne { get; private set; }
        public uint DimensionTwo { get; private set; }

        public CellPosition(uint dimensionOne, uint dimensionTwo)
        {
            DimensionOne = dimensionOne;
            DimensionTwo = dimensionTwo;
        }
    }
}
