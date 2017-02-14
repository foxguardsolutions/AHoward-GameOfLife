using System.Collections.Generic;
using GameOfLife;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class CommandRunnerTests : BaseTests
    {
        private Mock<IRuleset> _mockRuleset;
        private Mock<IGridFactory> _mockGridFactory;
        private Mock<IGameAdvancer> _mockGameAdvancer;
        private Mock<IGridWriter> _mockGridWriter;
        private CommandRunner _commandRunner;

        [SetUp]
        public void SetUp()
        {
            _mockRuleset = Fixture.Freeze<Mock<IRuleset>>();
            _mockGridFactory = Fixture.Freeze<Mock<IGridFactory>>();
            _mockGameAdvancer = Fixture.Freeze<Mock<IGameAdvancer>>();
            _mockGridWriter = Fixture.Freeze<Mock<IGridWriter>>();

            _commandRunner = Fixture.Create<CommandRunner>();
        }

        [Test]
        public void Execute_GivenReloadCommand_CallsDefaultRuleAndGridMethods()
        {
            var mockGrid = Fixture.Create<Mock<SquareTileGrid>>();
            _mockGridFactory.Setup(g => g.CreateDefaultGrid(It.IsAny<ISettings>())).Returns(mockGrid.Object);

            _commandRunner.Execute(Command.Reload);

            _mockRuleset.Verify(r => r.SetDefaultRules(It.IsAny<ISettings>()));
            _mockGridFactory.Verify(g => g.CreateDefaultGrid(It.IsAny<ISettings>()));
            Assert.That(_commandRunner.Grid, Is.EqualTo(mockGrid.Object));
        }

        [Test]
        public void Execute_GivenStepCommand_CallsGameAdvancerStep()
        {
            _commandRunner.Execute(Command.Step);

            _mockGameAdvancer.Verify(g => g.Step(_commandRunner.Grid, _commandRunner.Rules));
        }

        [Test]
        public void Execute_GivenDisplayCommand_CallsGridWriterWriteGridTo()
        {
            _commandRunner.Execute(Command.Display);

            _mockGridWriter.Verify(g => g.WriteCurrentStateOf(_commandRunner.Grid, It.IsAny<Dictionary<LifeState, string>>()));
        }

        [Test]
        public void Execute_GivenQuitCommand_CallsNoGameMethods()
        {
            MakeMockBehaviorStrict();
            _commandRunner = Fixture.Create<CommandRunner>();

            _commandRunner.Execute(Command.Quit);
        }

        private void MakeMockBehaviorStrict()
        {
            _mockRuleset = MakeMockWithStrictBehavior<IRuleset>();
            _mockGridFactory = MakeMockWithStrictBehavior<IGridFactory>();
            _mockGameAdvancer = MakeMockWithStrictBehavior<IGameAdvancer>();
            _mockGridWriter = MakeMockWithStrictBehavior<IGridWriter>();
        }

        private Mock<T> MakeMockWithStrictBehavior<T>()
            where T : class
        {
            var mock = new Mock<T>(MockBehavior.Strict);
            Fixture.Register(() => mock.Object);
            return mock;
        }
    }
}
