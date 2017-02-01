using GameOfLife;
using NUnit.Framework;

namespace GameOfLifeTests
{
    public class GameGridPatternTests : GameTests
    {
        [Test]
        public void GetCurrentPattern_BeforeAnySteps_ReturnsPatternWithoutChanges()
        {
            // Game.Load(Seed);
            // ^ Mock this
            var pattern = Game.GetCurrentPattern();

            Assert.That(pattern, Is.EqualTo(GliderPatternOne));
        }

        [Test]
        public void GetCurrentPattern_AfterSteps_ReturnsAdvancedPattern()
        {
            // Game.Load(Seed);
            // ^ Mock this

            // Game.SetRuleFor(LifeState.Alive, 2, 3);
            // Game.SetRuleFor(LifeState.Dead, 3);
            // ^ Mock this
            Game.Step();
            var pattern = Game.GetCurrentPattern();

            Assert.That(pattern, Is.EqualTo(GliderPatternTwo));
        }

        private static LifeState[][] GliderPatternOne
        {
            get
            {
                var pattern = new LifeState[5][];
                for (int rowNumber = 0; rowNumber < pattern.Length; rowNumber++)
                    pattern[rowNumber] = new LifeState[10];

                pattern[0][4] = LifeState.Alive;
                pattern[1][2] = LifeState.Alive;
                pattern[1][4] = LifeState.Alive;
                pattern[2][3] = LifeState.Alive;
                pattern[2][4] = LifeState.Alive;
                return pattern;
            }
        }

        private static LifeState[][] GliderPatternTwo
        {
            get
            {
                var pattern = new LifeState[5][];
                for (int rowNumber = 0; rowNumber < pattern.Length; rowNumber++)
                    pattern[rowNumber] = new LifeState[10];

                pattern[0][3] = LifeState.Alive;
                pattern[1][4] = LifeState.Alive;
                pattern[1][5] = LifeState.Alive;
                pattern[2][3] = LifeState.Alive;
                pattern[2][4] = LifeState.Alive;
                return pattern;
            }
        }
    }
}
