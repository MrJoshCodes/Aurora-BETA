using DotNetty.Common.Utilities;
using System;
using System.Collections.Generic;

namespace AuroraEmu.Game.Rooms.Pathfinder
{
    public static class Pathfinder
    {
        public static List<Point2D> GetPath(Room room, Point2D start, Point2D end)
        {
            List<Point2D> steps = new List<Point2D>();

            var path = FindReversePath(room, start, end);

            Node current = path;
            while (current != null)
            {
                steps.Add(current.Position);
                current = current.Next;
            }

            return steps;
        }

        public static Point2D[] DiagMovePoints = new[]
        {
            new Point2D(-1, -1),
            new Point2D(0, -1),
            new Point2D(1, -1),
            new Point2D(1, 0),
            new Point2D(1, 1),
            new Point2D(0, 1),
            new Point2D(-1, 1),
            new Point2D(-1, 0)
        };

        public static Node FindReversePath(Room room, Point2D start, Point2D end)
        {
            PriorityQueue<Node> openList = new PriorityQueue<Node>();
            var brWorld = new Node[room.Map.MapSize.Item1, room.Map.MapSize.Item2];
            Node node;
            Point2D tmp;
            int cost, diff;

            Node current = new Node(start);
            current.cost = 0;

            Node finish = new Node(end);
            brWorld[current.Position.X, current.Position.Y] = current;
            openList.Enqueue(current);

            while (openList.Count > 0)
            {
                current = openList.Dequeue();
                current.onClosedList = true;

                for (int i = 0; i < 8; i++)
                {
                    tmp = current.Position + DiagMovePoints[i];
                    try
                    {
                        if (!room.BlockedTiles[tmp.X, tmp.Y] && room.Map.PassableTiles[tmp.X, tmp.Y])
                        {
                            if (brWorld[tmp.X, tmp.Y] == null)
                            {
                                node = new Node(tmp);
                                brWorld[tmp.X, tmp.Y] = node;
                            }
                            else
                            {
                                node = brWorld[tmp.X, tmp.Y];
                            }

                            if (!node.onClosedList)
                            {
                                diff = 0;

                                if (current.Position.X != node.Position.X)
                                {
                                    diff += 1;
                                }

                                if (current.Position.Y != node.Position.Y)
                                {
                                    diff += 1;
                                }

                                cost = current.cost + diff + GetSquaredDistance(node.Position, end);

                                if (cost < node.cost)
                                {
                                    node.cost = cost;
                                    node.Next = current;
                                }

                                if (!node.onOpenList)
                                {
                                    if (node.Equals(finish))
                                    {
                                        node.Next = current;
                                        return node;
                                    }

                                    node.onOpenList = true;
                                    openList.Enqueue(node);
                                }
                            }
                        }
                    }
                    catch (IndexOutOfRangeException) { }
                }
            }

            return null;
        }

        private static int GetSquaredDistance(Point2D point, Point2D p1)
        {
            int dx = p1.X - point.X;
            int dy = p1.Y - point.Y;

            return (dx * dx) + (dy * dy);
        }

        public static int CalculateRotation(int x, int y, int newX, int newY, bool reversed = false)
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
    }
}