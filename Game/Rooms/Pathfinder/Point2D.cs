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

        public Point2D GetSquareInFront(int rotation) 
        {
            Point2D square = new Point2D(this.X, this.Y, this.Z);

            if (rotation == 0)
                square.Y--;
            else if (rotation == 2)
                square.X++;
            else if (rotation == 4)
                square.Y++;
            else if (rotation == 6)
                square.X--;

            return square;
        }

        public bool Equals(Point2D other)
        {
            return (X == other.X && Y == other.Y);
        }
    }
}