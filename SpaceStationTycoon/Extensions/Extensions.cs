using System;
using System.Collections.Concurrent;

namespace SpaceStationTycoon.Extensions
{
    public static class Extensions
    {
        public static void Clear<T>(this BlockingCollection<T> blockingCollection) {
            if (blockingCollection == null) {
                throw new ArgumentNullException("blockingCollection");
            }

            while (blockingCollection.Count > 0) {
                T item;
                blockingCollection.TryTake(out item);
            }
        }
    }
}