using GameOfLife;
using NUnit.Framework;

namespace GameOfLifeTests
{
    public class SquareTileGridTests : BaseTests
    {
        protected GridFactory GridFactory { get; private set; }

        [SetUp]
        public void SetUpGridFactory()
        {
            GridFactory = new GridFactory();
        }
    }
}
