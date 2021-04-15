using System;
using System.Collections.Generic;
using System.Linq;

namespace Bb.Galileo
{
    public static class ListExtension
    {

        public static List<T> RemoveWhere<T>(this List<T> self, Func<T, bool> predicate)
        {

            List<T> removed = new List<T>(self.Count);
            List<T> toRemove = self.Where(predicate).ToList();
            foreach (var item in toRemove)
            {
                self.Remove(item);
                removed.Add(item);
            }
            return removed;

        }

    }



}
