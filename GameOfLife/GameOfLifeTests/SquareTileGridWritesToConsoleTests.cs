using System;
using GameOfLife;
using Moq;
using NUnit.Framework;

namespace GameOfLifeTests
{
    public class SquareTileGridWritesToConsoleTests : SquareTileGridIterationTests
    {
        private Mock<IConsole> _consoleMock;

        [SetUp]
        public void SetUpMock()
        {
            _consoleMock = new Mock<IConsole>();

            // verify that newline is written the same number of times as the number of rows in _cells.
            // Verify that the total number of calls to write is number of rows * (number of columns + 1)
        }

        [Test]
        public void WritePatternToConsole_GivenCurrentPattern_WritesToConsoleExpectedNumberOfTimes()
        {
            var expectedNumberOfNewLines = Cells.GetLength(0);
            var expectedNumberOfCellsWritten = Cells.GetLength(0) * Cells.GetLength(1);
            var expectedNumberOfTotalWrites = expectedNumberOfCellsWritten + expectedNumberOfNewLines;

            Grid.WritePatternToConsole(Grid.GetCurrentPattern(), _consoleMock.Object);

            _consoleMock.Verify(c => c.Write(Environment.NewLine), Times.Exactly(expectedNumberOfNewLines));
            _consoleMock.Verify(c => c.Write(It.IsAny<string>()), Times.Exactly(expectedNumberOfTotalWrites));
        }
    }
}
