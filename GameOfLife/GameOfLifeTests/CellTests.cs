using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class CellTests : BaseTests
    {
        private LifeState _initialState;
        private Cell _cell;

        [SetUp]
        public void SetUp()
        {
            _initialState = Fixture.Create<LifeState>();
            _cell = new Cell(_initialState);
        }

        [Test]
        public void CurrentStateGetter_ReturnsInitialState()
        {
            var currentState = _cell.CurrentState;

            Assert.That(currentState, Is.EqualTo(_initialState));
        }

        [Test]
        public void AdvanceState_SetsCurrentStateToValueSetForNextState()
        {
            var state = Fixture.Create<LifeState>();
            _cell.SetNextState(state);

            _cell.AdvanceState();
            var currentState = _cell.CurrentState;

            Assert.That(currentState, Is.EqualTo(state));
        }
    }
}
