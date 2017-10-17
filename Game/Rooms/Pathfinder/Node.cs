using AuroraEmu.Utilities.Queue;
using System;

namespace AuroraEmu.Game.Rooms.Pathfinder
{
    public sealed class Node : FastPriorityQueueNode, IComparable<Node>
    {
        public int X { get; }
        public int Y { get; }
        public bool onClosedList = false;
        public bool onOpenList = false;
        public int cost = int.MaxValue;

        public Node Next { get; set; }

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public int CompareTo(Node Other)
        {
            return cost.CompareTo(Other.cost);
        }
    }
}