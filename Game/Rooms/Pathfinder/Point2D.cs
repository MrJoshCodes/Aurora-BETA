using System;

namespace AuroraEmu.Game.Rooms.Pathfinder
{
    public class Point2D : IEquatable<Point2D>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double Z { get; set; }

        public Point2D(int x, int y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Point2D other)
        {
            return (X == other.X && Y == other.Y);
        }
    }
}