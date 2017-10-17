using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.Utilities.Queue;
using System;
using System.Collections.Generic;

namespace AuroraEmu.Game.Rooms.Pathfinder
{
    public static class Pathfinder
    {
        public static List<Point2D> GetPath(Room room, Point2D start, Point2D end, RoomActor actor, bool retry = false)
        {
            List<Point2D> steps = new List<Point2D>();

            var path = FindReversePath(room, start, end, actor, retry);

            Node current = path;
            while (current != null)
            {
                steps.Add(new Point2D(current.X, current.Y));
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

        public static Node FindReversePath(Room room, Point2D start, Point2D end, RoomActor actor, bool retry)
        {
            FastPriorityQueue<Node> openList = new FastPriorityQueue<Node>(256);
            
            var brWorld = new Node[room.Map.MapSize.X, room.Map.MapSize.Y];
            Node node;
            int cost, diff, tmpX, tmpY;
            Node current = new Node(start.X, start.Y)
            {
                cost = 0
            };

            Node finish = new Node(end.X, end.Y);
            brWorld[current.X, current.Y] = current;

            openList.Enqueue(current, 0);

            while (openList.Count > 0)
            {
                current = openList.Dequeue();
                current.onClosedList = true;

                for (int i = 0; i < 8; i++)
                {
                    tmpX = current.X + DiagMovePoints[i].X;
                    tmpY = current.Y + DiagMovePoints[i].Y;

                    try
                    {
                        if (room.Grid.ValidStep(tmpX, tmpY, actor, retry))
                        {
                            if (brWorld[tmpX, tmpY] == null)
                            {
                                brWorld[tmpX, tmpY] = node = new Node(tmpX, tmpY);
                            }
                            else
                            {
                                node = brWorld[tmpX, tmpY];
                            }

                            if (!node.onClosedList)
                            {
                                diff = 0;

                                if (current.X != node.X)
                                {
                                    diff += 1;
                                }

                                if (current.Y != node.Y)
                                {
                                    diff += 1;
                                }

                                cost = current.cost + diff + GetSquaredDistance(node.X, node.Y, end.X, end.Y);

                                if (cost < node.cost)
                                {
                                    node.cost = cost;
                                    node.Next = current;
                                }

                                if (!node.onOpenList)
                                {
                                    if (node.X == finish.X && node.Y == finish.Y)
                                    {
                                        node.Next = current;
                                        return node;
                                    }

                                    node.onOpenList = true;
                                    openList.Enqueue(node, node.cost);
                                }
                            }
                        }
                    }
                    catch (IndexOutOfRangeException) { }
                }
            }

            return null;
        }

        private static int GetSquaredDistance(int x1, int y1, int x2, int y2)
        {
            int dx = x1 - x2;
            int dy = y1 - y2;

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