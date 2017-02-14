namespace GameOfLife
{
    public class HexTileGridB2S35Settings : HexTileGridSettings, ISettings
    {
        public uint[] SurvivalNumbers
        {
            get { return new uint[] { 3, 5 }; }
        }

        public uint[] ReproductionNumbers
        {
            get { return new uint[] { 2 }; }
        }

        protected override LifeState[,] MakeDefaultSeed()
        {
            var seed = new LifeState[10, 16];
            seed[1, 3] = LifeState.Alive;
            seed[2, 5] = LifeState.Alive;
            seed[3, 1] = LifeState.Alive;
            seed[3, 3] = LifeState.Alive;
            seed[3, 6] = LifeState.Alive;
            seed[5, 1] = LifeState.Alive;
            seed[5, 3] = LifeState.Alive;
            seed[5, 6] = LifeState.Alive;
            seed[6, 5] = LifeState.Alive;
            seed[7, 3] = LifeState.Alive;
            return seed;
        }
    }
}
