using System;

namespace AuroraEmu.Utilities.Queue
{
    internal interface IFixedSizePriorityQueue<TItem, in TPriority> : IPriorityQueue<TItem, TPriority>
        where TPriority : IComparable<TPriority>
    {
        void Resize(int maxNodes);

        int MaxSize { get; }
    }
}