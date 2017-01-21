using System;
using static System.Math;

namespace GameOfLife
{
    public class GridFactory
    {
        public Grid Create(LifeState[,] seed, params bool[] inputWrappingRules)
        {
            var dimensions = 2;
            var gridWrappingRules = new bool[dimensions];

            Array.Copy(inputWrappingRules, gridWrappingRules, Min(dimensions, inputWrappingRules.Length));

            return new Grid(seed, gridWrappingRules);
        }
    }
}
