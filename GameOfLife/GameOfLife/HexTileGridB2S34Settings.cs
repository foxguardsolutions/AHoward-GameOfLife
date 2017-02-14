namespace GameOfLife
{
    public class HexTileGridB2S34Settings : HexTileGridSettings, ISettings
    {
        public uint[] SurvivalNumbers
        {
            get { return new uint[] { 3, 4 }; }
        }

        public uint[] ReproductionNumbers
        {
            get { return new uint[] { 2 }; }
        }

        protected override LifeState[,] MakeDefaultSeed()
        {
            var seed = new LifeState[16, 32];
            seed[1, 6] = LifeState.Alive;
            seed[2, 5] = LifeState.Alive;
            seed[2, 8] = LifeState.Alive;
            seed[4, 3] = LifeState.Alive;
            seed[5, 1] = LifeState.Alive;
            seed[6, 3] = LifeState.Alive;
            seed[7, 28] = LifeState.Alive;
            seed[8, 26] = LifeState.Alive;
            seed[8, 27] = LifeState.Alive;
            seed[8, 28] = LifeState.Alive;
            seed[8, 29] = LifeState.Alive;
            seed[9, 26] = LifeState.Alive;
            seed[9, 27] = LifeState.Alive;
            seed[10, 27] = LifeState.Alive;
            return seed;
        }
    }
}
