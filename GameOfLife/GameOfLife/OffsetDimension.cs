using System.Collections.Generic;

namespace GameOfLife
{
    public class OffsetDimension : Dimension
    {
        public OffsetDimension(uint size, bool wraps)
            : base(size, wraps)
        {
        }

        public IEnumerable<uint> GetHighNeighborValues(uint currentValue)
        {
            var isLowestValue = currentValue == Min;
            var isHighestValue = currentValue == Max;

            if (isLowestValue && isHighestValue)
                return GetNeighborsOfOnlyValue();

            if (isHighestValue)
                return GetHighNeighborsOfHighestValue();

            return GetHighNeighborsOfNonHighestValue(currentValue);
        }

        private IEnumerable<uint> GetHighNeighborsOfHighestValue()
        {
            yield return Max;
            if (Wraps)
                yield return Min;
        }

        private IEnumerable<uint> GetHighNeighborsOfNonHighestValue(uint currentValue)
        {
            if (IsInRange(currentValue))
            {
                yield return currentValue;
                yield return currentValue + 1;
            }
        }

        public IEnumerable<uint> GetLowNeighborValues(uint currentValue)
        {
            var isLowestValue = currentValue == Min;
            var isHighestValue = currentValue == Max;

            if (isLowestValue && isHighestValue)
                return GetNeighborsOfOnlyValue();

            if (isLowestValue)
                return GetLowNeighborsOfLowestValue();

            return GetLowNeighborsOfNonLowestValue(currentValue);
        }

        private IEnumerable<uint> GetLowNeighborsOfLowestValue()
        {
            yield return Min;
            if (Wraps)
                yield return Max;
        }

        private IEnumerable<uint> GetLowNeighborsOfNonLowestValue(uint currentValue)
        {
            if (IsInRange(currentValue))
            {
                yield return currentValue - 1;
                yield return currentValue;
            }
        }
    }
}
