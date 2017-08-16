namespace AuroraEmu.Game.Rooms.Pathfinder
{
    internal sealed class Node
    {
        public Point2D Position { get; }
        public float ExpectedCost { get; set; }
        public float CostSoFar { get; set; }

        public Node Next { get; set; }
        public Node NextListElement { get; set; }

        public Node(Point2D position)
            : this(position, 0, 0)
        {
        }

        public Node(Point2D position, float expectedCost, float costSoFar)
        {
            Position = position;
            ExpectedCost = expectedCost;
            CostSoFar = costSoFar;
        }
    }
}