namespace AuroraEmu.Game.Rooms.Pathfinder
{
    public class Point2D
    {
        public int X;
        public int Y;

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point2D(Point2D point1, Point2D point2)
        {
            X = point1.X + point2.X;
            Y = point1.Y + point2.Y;
        }

        public int GetSquaredDistance(Point2D point)
        {
            int dx = X - point.X;
            int dy = Y - point.Y;

            return (dx * dx) + (dy * dy);
        }

        public bool Equals(Point2D p)
        {
            return p.X == this.X && p.Y == this.Y;
        }


        public static bool operator ==(Point2D one, Point2D two)
        {
            return one.Equals(two);
        }

        public static bool operator !=(Point2D one, Point2D two)
        {
            return !one.Equals(two);
        }

        public static Point2D operator +(Point2D one, Point2D two)
        {
            return new Point2D(one.X + two.X, one.Y + two.Y);
        }

        public static Point2D operator -(Point2D one, Point2D two)
        {
            return new Point2D(one.X - two.X, one.Y - two.Y);
        }
    }
}
