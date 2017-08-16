namespace AuroraEmu.Game.Rooms.Pathfinder
{
    public static class MovementPatterns
    {
        public static readonly Point2D[] Full =
        {
            new Point2D(-1, -1), new Point2D(0, -1), new Point2D(1, -1),
            new Point2D(-1, 0), new Point2D(1, 0),
            new Point2D(-1, 1), new Point2D(0, 1), new Point2D(1, 1)
        };

        public static readonly Point2D[] DiagonalDisabled =
        {
            new Point2D(0, -1),
            new Point2D(-1, 0), new Point2D(1, 0),
            new Point2D(0, 1),
        };
    }
}