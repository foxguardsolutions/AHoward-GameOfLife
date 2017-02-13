using System.Collections.Generic;
using System.Linq;
using GameOfLife;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class GridWriterTests : BaseTests
    {
        protected Mock<IConsoleWriter> MockConsole { get; private set; }
        protected Dictionary<LifeState, string> StateRepresentations { get; private set; }
        protected Dictionary<LifeState, string> SampleStateRepresentations
        {
            get
            {
                return new Dictionary<LifeState, string>()
                {
                    { LifeState.Alive, "*" },
                    { LifeState.Dead, "-" }
                };
            }
        }

        [SetUp]
        public void SetUp()
        {
            MockConsole = Fixture.Freeze<Mock<IConsoleWriter>>();

            StateRepresentations = new Dictionary<LifeState, string>()
            {
                { LifeState.Alive, Fixture.Create<string>() },
                { LifeState.Dead, Fixture.Create<string>() }
            };
        }

        protected static void SetToAlive(Cell cell)
        {
            cell.SetNextState(LifeState.Alive);
            cell.AdvanceState();
        }

        protected static Cell[][] MakeDeadCells(uint numberOfRows, uint numberOfColumns)
        {
            var cells = new Cell[numberOfRows][];
            for (int row = 0; row < numberOfRows; row++)
                cells[row] = MakeDeadCells(numberOfColumns).ToArray();

            return cells;
        }

        protected static IEnumerable<Cell> MakeDeadCells(uint numberOfCells)
        {
            for (int numberAlreadyMade = 0; numberAlreadyMade < numberOfCells; numberAlreadyMade++)
                yield return new Cell(LifeState.Dead);
        }
    }
}
