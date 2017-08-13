namespace AuroraEmu.Game.Rooms.Pathfinder
{
    public class Node
    {
        public Point2D Position;
        public int Cost;
        public int PathCost;
        public Node Next;
        public Node NextListElement;

        public Node(Point2D position, int cost, int pathCost, Node next)
        {
            Position = position;
            Cost = cost;
            PathCost = pathCost;
            Next = next;
        }
    }
}
