using System;
using System.Collections.Generic;
using GameOfLife;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class HexTileGridWriterTests : GridWriterTests
    {
        private HexTileGridWriter _gridWriter;

        [SetUp]
        public void SetUpGridWriter()
        {
            _gridWriter = Fixture.Create<HexTileGridWriter>();
        }

        [Test]
        public void WriteCurrentStateOfAnyGrid_WritesToConsole()
        {
            var grid = Fixture.Create<HexTileGrid>();

            _gridWriter.WriteCurrentStateOf(grid, StateRepresentations);

            MockConsole.Verify(c => c.WriteLine(It.IsAny<string>()));
        }

        [Test]
        public void WriteCurrentStateOfKnownGrid_WritesCorrectStringToConsole()
        {
            var grid = GridWithTwoAndFourStepOscillators;

            _gridWriter.WriteCurrentStateOf(grid, SampleStateRepresentations);

            MockConsole.Verify(c => c.WriteLine(RepresentationOfGridWithTwoAndFourStepOscillators));
        }

        private static IGrid GridWithTwoAndFourStepOscillators
        {
            get
            {
                var grid = new HexTileGrid(MakeDeadCells(8, 8), true, true);
                foreach (var position in TwoAndFourStepOscillatorPatternLiveCellPositions())
                    SetToAlive(grid.GetCellAt(position));

                return grid;
            }
        }

        private static string RepresentationOfGridWithTwoAndFourStepOscillators
        {
            get
            {
                var representation =
                    "- - - - - - - -{0}" +
                    " - - - - - * - *{0}" +
                    "- - - - - - * *{0}" +
                    " - - - - - - - -{0}" +
                    "- - * - - - - -{0}" +
                    " - - - * - - - -{0}" +
                    "- - * - - - - -{0}" +
                    " - - - - - - - -{0}";
                return string.Format(representation, Environment.NewLine);
            }
        }

        private static IEnumerable<CellPosition> TwoAndFourStepOscillatorPatternLiveCellPositions()
        {
            yield return new CellPosition(1, 5);
            yield return new CellPosition(1, 7);
            yield return new CellPosition(2, 6);
            yield return new CellPosition(2, 7);
            yield return new CellPosition(4, 2);
            yield return new CellPosition(5, 3);
            yield return new CellPosition(6, 2);
        }
    }
}
