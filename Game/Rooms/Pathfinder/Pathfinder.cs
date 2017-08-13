using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraEmu.Game.Rooms.Pathfinder
{
    public class Pathfinder
    {
        public static List<Point2D> FindPath(Room room, RoomMap roomMap, Point2D start, Point2D end)
        {
            List<Point2D> path = new List<Point2D>();

            Node nodes = FindReversedPath(room, roomMap, start, end);

            if (nodes != null)
            {
                while (nodes.Next != null)
                {
                    path.Add(nodes.Next.Position);
                    nodes = nodes.Next;
                }
            }

            return path;
        }

        private static Node FindReversedPath(Room room, RoomMap roomMap, Point2D start, Point2D end)
        {
            Node startNode = new Node(start, 0, 0, null);

            Node[,] map = new Node[roomMap.MapSize.Item1, roomMap.MapSize.Item2];

            BinaryHeap openList = new BinaryHeap();
            openList.Add(startNode);

            while (openList.HasNext())
            {
                Node current = openList.ExtractFirst();

                if (current.Position.GetSquaredDistance(end) <= 3)
                {
                    return new Node(end, current.PathCost + 1, current.Cost + 1, current);
                }

                for (int i = 0; i < (room.DiagEnabled ? surroundingDiag.Length : surroundingNoDiag.Length); i++)
                {
                    Surrounding surr = (room.DiagEnabled ? surroundingDiag[i] : surroundingNoDiag[i]);
                    Point2D tmp = new Point2D(current.Position, surr.Point);

                    try
                    {
                        if (map[tmp.X, tmp.Y] == null)
                        {
                            int pathCost = current.PathCost + surr.Cost;
                            int cost = pathCost + tmp.GetSquaredDistance(end);
                            Node node = new Node(tmp, cost, pathCost, current);
                            openList.Add(node);
                        }
                    }
                    catch (IndexOutOfRangeException ex)
                    {

                    }
                }
            }

            return null;
        }

        public class Surrounding
        {
            public Point2D Point;
            public int Cost;

            public Surrounding(int x, int y)
            {
                Point = new Point2D(x, y);
                Cost = x * x + y * y;
            }
        }

        public static int CalculateRotation(int x, int y, int newX, int newY, bool reversed)
        {
            int rotation = 0;

            if (x > newX && y > newY)
                rotation = 7;
            else if (x < newX && y < newY)
                rotation = 3;
            else if (x > newX && y < newY)
                rotation = 5;
            else if (x < newX && y > newY)
                rotation = 1;
            else if (x > newX)
                rotation = 6;
            else if (x < newX)
                rotation = 2;
            else if (y < newY)
                rotation = 4;
            else if (y > newY)
                rotation = 0;

            if (reversed)
            {
                if (rotation > 3)
                {
                    rotation = rotation - 4;
                }
                else
                {
                    rotation = rotation + 4;
                }
            }

            return rotation;
        }

        private static Surrounding[] surroundingDiag = new Surrounding[]
        {
            new Surrounding(1, 0),
            new Surrounding(0, 1),
            new Surrounding(-1, 0),
            new Surrounding(0, -1),

            new Surrounding(1, 1),
            new Surrounding(-1, -1),
            new Surrounding(1, -1),
            new Surrounding(-1, 1)
        };

        private static Surrounding[] surroundingNoDiag = new Surrounding[]
        {
            new Surrounding(1, 0),
            new Surrounding(0, 1),
            new Surrounding(-1, 0),
            new Surrounding(0, -1),
        };
    }
}
