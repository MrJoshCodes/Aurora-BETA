using System;
using System.Collections;
using System.Collections.Generic;

namespace AuroraEmu.Utilities
{
    public class PriorityQueue<T> : IEnumerable<T>
        where T : class
    {
        readonly IComparer<T> comparer;
        int count;
        int capacity;
        T[] items;

        public PriorityQueue(IComparer<T> comparer)
        {
            this.comparer = comparer;
            capacity = 11;
            items = new T[capacity];
        }

        public int Count => count;

        public T DequeueLowest()
        {
            T result = GetFirst();
            if (result == null)
            {
                return null;
            }

            int newCount = --count;
            T lastItem = items[newCount];
            items[newCount] = null;
            if (newCount > 0)
            {
                TrickleDown(0, lastItem);
            }

            return result;
        }

        public T DequeueHighest()
        {
            T result = GetLast();
            if (result == null)
            {
                return null;
            }

            int newCount = --count;
            T lastItem = items[newCount];
            items[newCount] = null;
            if (newCount > 0)
            {
                TrickleDown(0, lastItem);
            }

            return result;
        }

        public T GetFirst() => count == 0 ? null : items[0];

        public T GetLast() => count == 0 ? null : items[count];

        public void Enqueue(T item)
        {
            int oldCount = count;
            if (oldCount == capacity)
            {
                GrowHeap();
            }
            count = oldCount + 1;
            BubbleUp(oldCount, item);
        }

        public void Remove(T item)
        {
            int index = Array.IndexOf(items, item);
            if (index == -1)
            {
                return;
            }

            count--;
            if (index == count)
            {
                items[index] = default(T);
            }
            else
            {
                T last = items[count];
                items[count] = default(T);
                TrickleDown(index, last);
                if (items[index] == last)
                {
                    BubbleUp(index, last);
                }
            }
        }

        void BubbleUp(int index, T item)
        {
            // index > 0 means there is a parent
            while (index > 0)
            {
                int parentIndex = (index - 1) >> 1;
                T parentItem = items[parentIndex];
                if (comparer.Compare(item, parentItem) >= 0)
                {
                    break;
                }
                items[index] = parentItem;
                index = parentIndex;
            }
            items[index] = item;
        }

        void GrowHeap()
        {
            int oldCapacity = capacity;
            capacity = oldCapacity + (oldCapacity <= 64 ? oldCapacity + 2 : (oldCapacity >> 1));
            var newHeap = new T[capacity];
            Array.Copy(items, 0, newHeap, 0, count);
            items = newHeap;
        }

        void TrickleDown(int index, T item)
        {
            int middleIndex = count >> 1;
            while (index < middleIndex)
            {
                int childIndex = (index << 1) + 1;
                T childItem = items[childIndex];
                int rightChildIndex = childIndex + 1;
                if (rightChildIndex < count
                    && comparer.Compare(childItem, items[rightChildIndex]) > 0)
                {
                    childIndex = rightChildIndex;
                    childItem = items[rightChildIndex];
                }
                if (comparer.Compare(item, childItem) <= 0)
                {
                    break;
                }
                items[index] = childItem;
                index = childIndex;
            }
            items[index] = item;
        }

        public void Clear()
        {
            count = 0;
            Array.Clear(items, 0, 0);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}