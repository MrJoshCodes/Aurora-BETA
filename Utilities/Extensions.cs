using System;
using System.Collections.Generic;
using System.Text;

namespace AuroraEmu.Utilities
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerator, Action<T> action)
        {
            foreach (T item in enumerator)
            {
                action(item);
            }
        }
    }
}
