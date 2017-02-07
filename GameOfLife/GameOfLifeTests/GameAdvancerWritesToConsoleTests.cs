using GameOfLife;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class GameAdvancerWritesToConsoleTests : GameAdvancerTests
    {
        private Mock<IConsoleWriter> _mockConsole;
        private Mock<IGrid> _mockGrid;

        protected override void SetUpMockConsole()
        {
            _mockConsole = new Mock<IConsoleWriter>(MockBehavior.Strict);
            Fixture.Register(() => _mockConsole.Object);
        }

        [SetUp]
        public void SetUpMockGrid()
        {
            _mockGrid = Fixture.Freeze<Mock<IGrid>>();
        }

        [Test]
        public void Step_GivenIncompleteRules_WritesIncompleteRulesMessageToConsole()
        {
            var expectedMessage = "No steps taken.  Cannot step until rules have been defined for all possible cell states.";
            _mockConsole.Setup(c => c.WriteLine(expectedMessage));
            GivenRulesetWithCompletionStatus(false);

            Advancer.Step(_mockGrid.Object, MockRuleset.Object);

            _mockConsole.Verify(c => c.WriteLine(expectedMessage));
        }

        [Test]
        public void Step_GivenCompleteRules_DoesNotWriteToConsole()
        {
            GivenRulesetWithCompletionStatus(true);

            Advancer.Step(_mockGrid.Object, MockRuleset.Object);
        }
    }
}
