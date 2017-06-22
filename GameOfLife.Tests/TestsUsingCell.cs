using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLife.Tests
{
    [TestFixture]
    public class TestsUsingCell : Tests
    {
        protected Cell Cell;

        [SetUp]
        public new void SetUp()
        {
            Cell = Fixture.Create<Cell>();
        }

        protected void GivenDeadCell()
        {
            Cell.Alive = false;
        }

        protected void GivenLiveCell()
        {
            Cell.Alive = true;
        }
    }
}
