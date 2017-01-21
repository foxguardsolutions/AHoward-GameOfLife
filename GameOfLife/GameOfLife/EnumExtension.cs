using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public static class EnumExtension
    {
        public static bool ForEvery<EnumType>(Func<EnumType, bool> predicate)
        {
            var enumValues = (IEnumerable<EnumType>)Enum.GetValues(typeof(EnumType));
            return enumValues.All(predicate);
        }
    }
}
