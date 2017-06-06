using NUnit.Framework;
using Ploeh.AutoFixture;
using System.Collections.Generic;

namespace GameOfLife.Tests
{
    [TestFixture]
    public class Tests
    {
        protected static string AliveCellString => Cell.AliveCellString;
        protected static string DeadCellString => Cell.DeadCellString;
        protected static string NewLine => GridFactory.NewLine;
        protected Fixture Fixture { get; set; }

        [SetUp]
        public void SetUp()
        {
            Fixture = new Fixture();
        }

        protected static IEnumerable<bool> BooleanValues()
        {
            yield return true;
            yield return false;
        }
    }
}
