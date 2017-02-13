namespace GameOfLife
{
    public class SquareTileGridSettings : Settings, ISettings
    {
        public uint[] SurvivalNumbers
        {
            get { return new uint[] { 2, 3 }; }
        }

        public uint[] ReproductionNumbers
        {
            get { return new uint[] { 3 }; }
        }

        protected override LifeState[,] MakeDefaultSeed()
        {
            var seed = new LifeState[64, 64];
            seed[0, 27] = LifeState.Alive;
            seed[1, 26] = LifeState.Alive;
            seed[1, 28] = LifeState.Alive;
            seed[2, 9] = LifeState.Alive;
            seed[2, 10] = LifeState.Alive;
            seed[2, 26] = LifeState.Alive;
            seed[2, 27] = LifeState.Alive;
            seed[2, 29] = LifeState.Alive;
            seed[3, 9] = LifeState.Alive;
            seed[3, 11] = LifeState.Alive;
            seed[3, 26] = LifeState.Alive;
            seed[3, 27] = LifeState.Alive;
            seed[3, 29] = LifeState.Alive;
            seed[3, 30] = LifeState.Alive;
            seed[3, 34] = LifeState.Alive;
            seed[3, 35] = LifeState.Alive;
            seed[4, 4] = LifeState.Alive;
            seed[4, 5] = LifeState.Alive;
            seed[4, 12] = LifeState.Alive;
            seed[4, 26] = LifeState.Alive;
            seed[4, 27] = LifeState.Alive;
            seed[4, 29] = LifeState.Alive;
            seed[4, 34] = LifeState.Alive;
            seed[4, 35] = LifeState.Alive;
            seed[5, 0] = LifeState.Alive;
            seed[5, 1] = LifeState.Alive;
            seed[5, 3] = LifeState.Alive;
            seed[5, 6] = LifeState.Alive;
            seed[5, 9] = LifeState.Alive;
            seed[5, 12] = LifeState.Alive;
            seed[5, 26] = LifeState.Alive;
            seed[5, 28] = LifeState.Alive;
            seed[6, 0] = LifeState.Alive;
            seed[6, 1] = LifeState.Alive;
            seed[6, 4] = LifeState.Alive;
            seed[6, 5] = LifeState.Alive;
            seed[6, 12] = LifeState.Alive;
            seed[6, 27] = LifeState.Alive;
            seed[7, 9] = LifeState.Alive;
            seed[7, 11] = LifeState.Alive;
            seed[8, 9] = LifeState.Alive;
            seed[8, 10] = LifeState.Alive;
            return seed;
        }

        public IGrid GetDefaultGrid(IGridFactory gridFactory)
        {
            return gridFactory.CreateSquareTileGrid(DefaultSeed, false, true);
        }
    }
}
