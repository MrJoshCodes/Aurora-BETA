using System.Collections.Generic;
using System.Linq;

namespace AuroraEmu.Game.Rooms.Pathfinder
{
    internal static class PathFinder
    {
        public static Node FindReversePath(RoomMap map, Grid grid, Point2D start, Point2D end,
            Point2D[] movementPattern)
        {
            var head = new Node(start);
            var open = new BinaryHeap();
            open.Push(head);
            bool[,] walkableTiles = map.PassableTiles;

            while (open.HasNext())
            {
                var current = open.Pop();

                if (current.Position.Equals(end))
                {
                    return current;
                }

                foreach (var p in GetNeighbours(current.Position, grid.X, grid.Y, movementPattern))
                {
                    var cellCost = grid.GetCellCostUnchecked(p);
                    if (walkableTiles[current.Position.X, current.Position.Y] && !float.IsInfinity(cellCost))
                    {
                        var costSoFar = current.CostSoFar + cellCost;
                        var expectedCost = costSoFar + GetSquaredDistance(end, p);

                        open.Push(new Node(p, expectedCost, costSoFar) {Next = current});
                    }
                }
            }

            return null;
        }

        private static IEnumerable<Point2D> GetNeighbours(
            Point2D position,
            int dimX,
            int dimY,
            IEnumerable<Point2D> movementPattern)
        {
            return movementPattern.Select(n => new Point2D(position.X + n.X, position.Y + n.Y))
                .Where(p => p.X >= 0 && p.X < dimX && p.Y >= 0 && p.Y < dimY);
        }

        private static float GetSquaredDistance(Point2D point, Point2D p1)
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