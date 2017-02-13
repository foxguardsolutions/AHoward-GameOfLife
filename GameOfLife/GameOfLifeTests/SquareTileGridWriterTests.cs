using System;
using System.Collections.Generic;
using GameOfLife;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class SquareTileGridWriterTests : GridWriterTests
    {
        private SquareTileGridWriter _gridWriter;

        [SetUp]
        public void SetUpGridWriter()
        {
            _gridWriter = Fixture.Create<SquareTileGridWriter>();
        }

        [Test]
        public void WriteCurrentStateOfAnyGrid_WritesToConsole()
        {
            var grid = Fixture.Create<SquareTileGrid>();

            _gridWriter.WriteCurrentStateOf(grid, StateRepresentations);

            MockConsole.Verify(c => c.WriteLine(It.IsAny<string>()));
        }

        [Test]
        public void WriteCurrentStateOfKnownGrid_WritesCorrectStringToConsole()
        {
            var grid = GridWithGliderPattern;

            _gridWriter.WriteCurrentStateOf(grid, SampleStateRepresentations);

            MockConsole.Verify(c => c.WriteLine(RepresentationOfGridWithGliderPattern));
        }

        private static IGrid GridWithGliderPattern
        {
            get
            {
                var grid = new SquareTileGrid(MakeDeadCells(5, 10), true, true);
                foreach (var position in GliderPatternLiveCellPositions())
                    SetToAlive(grid.GetCellAt(position));

                return grid;
            }
        }

        private static string RepresentationOfGridWithGliderPattern
        {
            get
            {
                var representation =
                    "----*-----{0}" +
                    "--*-*-----{0}" +
                    "---**-----{0}" +
                    "----------{0}" +
                    "----------{0}";
                return string.Format(representation, Environment.NewLine);
            }
        }

        private static IEnumerable<CellPosition> GliderPatternLiveCellPositions()
        {
            yield return new CellPosition(0, 4);
            yield return new CellPosition(1, 2);
            yield return new CellPosition(1, 4);
            yield return new CellPosition(2, 3);
            yield return new CellPosition(2, 4);
        }
    }
}
