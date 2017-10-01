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
            return this.X == other.X && this.Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj is Point2D && Equals((Point2D) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static Point2D operator +(Point2D p1, Point2D p2)
        {
            return new Point2D(p1.X + p2.X, p1.Y + p2.Y);
        }
    }
}