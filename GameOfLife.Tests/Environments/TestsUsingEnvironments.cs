using NUnit.Framework;
using Ploeh.AutoFixture;
using System.Collections.Generic;

namespace GameOfLife.Tests.Environments
{
    [TestFixture]
    public class TestsUsingEnvironments : TestsUsingGrid
    {
        private const int BottomLeftCorner = 3;
        private const int BottomSide = 4;
        private const int LeftSide = 2;
        private const int RightSide = 3;
        private const int TopLeftCorner = 1;
        private const int TopRightCorner = 2;
        private const int TopSide = 1;

        protected int Column;
        protected int Row;

        [SetUp]
        public new void SetUp()
        {
            Column = Fixture.Create<int>() % Grid.Width;
            Row = Fixture.Create<int>() % Grid.Height;
        }

        protected static IEnumerable<int> EdgeNumbers()
        {
            yield return 1;
            yield return 2;
            yield return 3;
            yield return 4;
        }

        protected void GivenColumnAwayFromEdgeOfGrid()
        {
            if (Column == 0)
                Column++;
            if (Column == Grid.Width - 1)
                Column--;
        }

        protected void GivenCorner(int cornerNumber)
        {
            Row = cornerNumber == TopLeftCorner || cornerNumber == TopRightCorner ? 0 : Grid.Height - 1;
            Column = cornerNumber == TopLeftCorner || cornerNumber == BottomLeftCorner ? 0 : Grid.Width - 1;
        }

        protected void GivenRowAwayFromEdgeOfGrid()
        {
            if (Row == 0)
                Row++;
            if (Row == Grid.Height - 1)
                Row--;
        }

        protected void GivenSide(int sideNumber)
        {
            if (sideNumber == TopSide || sideNumber == BottomSide)
            {
                Row = sideNumber == TopSide ? 0 : Grid.Height - 1;
                GivenColumnAwayFromEdgeOfGrid();
            }
            else if (sideNumber == LeftSide || sideNumber == RightSide)
            {
                Column = sideNumber == LeftSide ? 0 : Grid.Width - 1;
                GivenRowAwayFromEdgeOfGrid();
            }
        }
    }
}
