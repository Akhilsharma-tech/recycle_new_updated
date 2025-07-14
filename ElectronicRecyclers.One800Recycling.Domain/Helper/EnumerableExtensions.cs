using System;
using System.Collections.Generic;

namespace ElectronicRecyclers.One800Recycling.Domain.Common.Helper
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            var random = new Random();
            var total = list.Count;
            while (total > 1)
            {
                total--;
                var index = random.Next(total + 1);
                var value = list[index];
                list[index] = list[total];
                list[total] = value;
            }

            return list;
        } 
    }
}