using System.Collections.Generic;

namespace AuroraEmu.Game.Rooms.Pathfinder
{
    public sealed class Grid
    {
        private readonly float _defaultCost;
        private readonly float[] _weights;

        public int X { get; }
        public int Y { get; }
        public RoomMap Map { get; }

        public Grid(RoomMap map, int x, int y, float defaultCost = 1.0f)
        {
            Map = map;
            _defaultCost = defaultCost;
            _weights = new float[x * y];
            X = x;
            Y = y;
            for (var n = 0; n < _weights.Length; n++)
            {
                _weights[n] = defaultCost;
            }

        }

        public void SetCellCost(Point2D position, float cost)
        {
            _weights[GetIndex(position.X, position.Y)] = cost;
        }

        public void BlockCell(Point2D position) => SetCellCost(position, float.PositiveInfinity);


        public void UnblockCell(Point2D position) => SetCellCost(position, _defaultCost);

        public double GetCellCost(Point2D position)
        {
            return _weights[GetIndex(position.X, position.Y)];
        }

        internal float GetCellCostUnchecked(Point2D position)
        {
            return _weights[X * position.Y + position.X];
        }

        public IList<Point2D> GetPath(Point2D start, Point2D end, Point2D[] movementPattern)
        {
            var steps = new List<Point2D>();

            var path = PathFinder.FindReversePath(Map, this, start, end, movementPattern);

            var current = path;
            while (current != null)
            {
                steps.Add(current.Position);
                current = current.Next;
            }

            return steps;
        }

        private int GetIndex(int x, int y)
        {
            return GetIndexUnchecked(x, y);
        }

        internal int GetIndexUnchecked(int x, int y) => X * y + x;
    }
}