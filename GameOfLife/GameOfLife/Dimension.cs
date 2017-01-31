using System.Collections.Generic;

namespace GameOfLife
{
    public class Dimension
    {
        private bool _wraps;
        public uint Min { get; private set; }
        public uint Max { get; private set; }

        public Dimension(uint size, bool wraps)
        {
            Min = 0;
            Max = size - 1;
            _wraps = wraps;
        }

        public IEnumerable<uint> GetNeighborValues(uint currentValue)
        {
            var isLowestValue = currentValue == Min;
            var isHighestValue = currentValue == Max;
            if (isLowestValue && isHighestValue)
                return GetNeighborsOfOnlyValue();

            if (isLowestValue)
                return GetNeighborsOfLowestValue();

            if (isHighestValue)
                return GetNeighborsOfHighestValue();

            return GetNeighborsOfNonEdgeValue(currentValue);
        }

        private IEnumerable<uint> GetNeighborsOfOnlyValue()
        {
            yield return Min;
        }

        private IEnumerable<uint> GetNeighborsOfLowestValue()
        {
            yield return Min;
            yield return Min + 1;
            if (_wraps)
                yield return Max;
        }

        private IEnumerable<uint> GetNeighborsOfHighestValue()
        {
            yield return Max - 1;
            yield return Max;
            if (_wraps)
                yield return Min;
        }

        private IEnumerable<uint> GetNeighborsOfNonEdgeValue(uint currentValue)
        {
            if (currentValue > Min && currentValue < Max)
            {
                yield return currentValue - 1;
                yield return currentValue;
                yield return currentValue + 1;
            }
        }

        public bool IsOwnNeighbor()
        {
            return Max == Min && _wraps;
        }
    }
}
