using System;
using GameOfLife;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace GameOfLifeTests
{
    public class GameExecutesStepTests : GameTests
    {
        [Test]
        public void CountGenerations_BeforeLoad_ReturnsZero()
        {
            var initialGenerationCount = Game.CountGenerations();

            Assert.That(initialGenerationCount, Is.EqualTo(0));
        }

        [Test]
        public void CountGenerations_AfterLoad_ReturnsOne()
        {
            // Game.Load(Seed);
            // ^ Mock this
            var generationCount = Game.CountGenerations();

            Assert.That(generationCount, Is.EqualTo(1));
        }

        [Test]
        public void Step_BeforeAnyRuleLoad_DoesNotPerformAnyAction()
        {
            // Game.Load(Seed);
            // ^ Mock this
            var initialGenerationCount = Game.CountGenerations();

            TakeSomeSteps();

            var finalGenerationCount = Game.CountGenerations();

            Assert.That(finalGenerationCount, Is.EqualTo(initialGenerationCount));
        }

        [Test]
        public void Step_BeforeCompleteRuleLoad_DoesNotPerformAnyAction()
        {
            // Game.Load(Seed);
            // ^ Mock this
            var initialGenerationCount = Game.CountGenerations();

            // Game.SetRuleFor(Fixture.Create<LifeState>());
            // ^ Mock this
            TakeSomeSteps();

            var finalGenerationCount = Game.CountGenerations();

            Assert.That(finalGenerationCount, Is.EqualTo(initialGenerationCount));
        }

        [Test]
        public void Step_AfterCompleteRuleLoad_SavesANewGeneration()
        {
            // Game.Load(Seed);
            // ^ Mock this
            var initialGenerationCount = Game.CountGenerations();

            // foreach (LifeState state in Enum.GetValues(typeof(LifeState)))
            //    Game.SetRuleFor(state);
            // ^ Mock this
            var numberOfSteps = TakeSomeSteps();

            var finalGenerationCount = Game.CountGenerations();

            Assert.That(finalGenerationCount, Is.EqualTo(initialGenerationCount + numberOfSteps));
        }

        private int TakeSomeSteps()
        {
            var numberOfSteps = Fixture.Create<int>();
            for (int i = 0; i < numberOfSteps; i++)
                Game.Step();

            return numberOfSteps;
        }
    }
}
