using GameOfLife.Operations;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GameOfLife.Tests.Operations
{
    [TestFixture]
    public class CommandFactoryTests
    {
        [TestCaseSource(nameof(CellOperationsAndExpectedTypes))]
        public void GetCommand_GivenCellOperation_ReturnsExpectedCommandType(CellOperation operation, Type expectedType)
        {
            var actual = CommandFactory.GetCommand(operation);

            Assert.That(actual, Is.TypeOf(expectedType));
        }

        private static IEnumerable<TestCaseData> CellOperationsAndExpectedTypes()
        {
            yield return new TestCaseData(CellOperation.BringToLife, typeof(BringToLifeCommand));
            yield return new TestCaseData(CellOperation.Kill, typeof(KillCommand));
            yield return new TestCaseData(CellOperation.NoAction, typeof(NoActionCommand));
        }
    }
}
