using System;

namespace AuroraEmu.Game.Rooms.Pathfinder
{
    public sealed class Node : IComparable<Node>
    {
        public Point2D Position { get; }
        public bool onClosedList = false;
        public bool onOpenList = false;
        public int cost = int.MaxValue;

        public Node Next { get; set; }

        public Node(Point2D position)
        {
            Position = position;
        }

        public override bool Equals(object obj)
        {
            return (obj is Node) && ((Node)obj).Position.Equals(this.Position);
        }

        public bool Equals(Node Breadcrumb)
        {
            return Breadcrumb.Position.Equals(this.Position);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }

        public int CompareTo(Node Other)
        {
            return cost.CompareTo(Other.cost);
        }
    }
}