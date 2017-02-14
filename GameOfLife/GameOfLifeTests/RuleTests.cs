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
        private Cell[] _neighbors;

        [SetUp]
        public void SetUp()
        {
            _numbersYieldingLive = Fixture.CreateMany<uint>().ToArray();

            var numberOfLiveNeighbors = Fixture.PickFromValues(_numbersYieldingLive);
            var numberOfDeadNeighbors = Fixture.Create<int>();
            _neighbors = CreateSomeLivingAndDeadCells((int)numberOfLiveNeighbors, numberOfDeadNeighbors).ToArray();
        }

        private IEnumerable<Cell> CreateSomeLivingAndDeadCells(int numberOfLiving, int numberOfDead)
        {
            var livingCells = CreateSomeCells(LifeState.Alive, numberOfLiving);
            var deadCells = CreateSomeCells(LifeState.Dead, numberOfDead);
            return livingCells.Concat(deadCells);
        }

        private IEnumerable<Cell> CreateSomeCells(LifeState cellState, int numberToMake)
        {
            Fixture.Register(() => cellState);
            return Fixture.CreateMany<Cell>(numberToMake);
        }

        [Test]
        public void Apply_GivenLiveNeighborCountInRuleCountsArray_ReturnsAlive()
        {
            var rule = new Rule(_numbersYieldingLive);

            var result = rule.Apply(_neighbors);

            Assert.That(result, Is.EqualTo(LifeState.Alive));
        }

        [Test]
        public void Apply_GivenLiveNeighborCountNotInRuleCountsArray_ReturnsDead()
        {
            var rule = new Rule(new uint[0]);

            var result = rule.Apply(_neighbors);

            Assert.That(result, Is.EqualTo(LifeState.Dead));
        }
    }
}
