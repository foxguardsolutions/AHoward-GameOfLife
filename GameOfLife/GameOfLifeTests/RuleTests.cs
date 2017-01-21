using System.Collections.Generic;
using System.Linq;
using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class RuleTests : BaseTests
    {
        private uint[] _numbersYieldingLive;
        private RuleFactory _ruleFactory;
        private Cell[] _cells;

        [SetUp]
        public void SetUp()
        {
            _numbersYieldingLive = Fixture.CreateMany<uint>().ToArray();
            _ruleFactory = Fixture.Create<RuleFactory>();
            _cells = MakeSomeLivingAndDeadCells(
                _numbersYieldingLive[0], Fixture.Create<uint>());
        }

        private Cell[] MakeSomeLivingAndDeadCells(uint numberOfLiving, uint numberOfDead)
        {
            var livingCells = MakeSomeCells(LifeState.Alive, numberOfLiving);
            var deadCells = MakeSomeCells(LifeState.Dead, numberOfDead);
            return livingCells.Concat(deadCells).ToArray();
        }

        private IEnumerable<Cell> MakeSomeCells(LifeState cellState, uint numberToMake)
        {
            for (int i = 0; i < numberToMake; i++)
                yield return MakeCellInState(cellState);
        }

        private Cell MakeCellInState(LifeState state)
        {
            var cell = Fixture.Create<Cell>();
            if (cell.CurrentState != state)
            {
                Fixture.Create<LifeState>();
                cell = Fixture.Create<Cell>();
            }

            return cell;
        }

        [Test]
        public void Apply_GivenLiveNeighborCountInRuleCountsArray_ReturnsAlive()
        {
            var rule = _ruleFactory.Create(_numbersYieldingLive);

            var result = rule.Apply(_cells);

            Assert.That(result, Is.EqualTo(LifeState.Alive));
        }

        [Test]
        public void Apply_GivenLiveNeighborCountNotInRuleCountsArray_ReturnsDead()
        {
            var rule = _ruleFactory.Create();

            var result = rule.Apply(_cells);

            Assert.That(result, Is.EqualTo(LifeState.Dead));
        }
    }
}
