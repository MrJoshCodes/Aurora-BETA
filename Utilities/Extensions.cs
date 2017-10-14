using AuroraEmu.Game.Rooms.Pathfinder;
using System;
using System.Collections.Generic;

namespace AuroraEmu.Utilities
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerator, Action<T> action)
        {
            foreach (T item in enumerator)
            {
                action(item);
            }
        }

        public static List<Point2D> AffectedTiles(int length, int width, int posX, int posY, int rotation)
        {

            List<Point2D> points = new List<Point2D>();

            if (length > 1)
            {
                if (rotation == 0 || rotation == 4)
                {
                    for (int i = 1; i < length; i++)
                    {
                        points.Add(new Point2D(posX, posY + i, i));

                        for (int j = 1; j < width; j++)
                        {
                            points.Add(new Point2D(posX + j, posY + i, (i < j) ? j : i));
                        }
                    }
                }
                else if (rotation == 2 || rotation == 6)
                {
                    for (int i = 1; i < length; i++)
                    {
                        points.Add(new Point2D(posX + i, posY, i));

                        for (int j = 1; j < width; j++)
                        {
                            points.Add(new Point2D(posX + i, posY + j, (i < j) ? j : i));
                        }
                    }
                }
            }

            if (width > 1)
            {
                if (rotation == 0 || rotation == 4)
                {
                    for (int i = 1; i < width; i++)
                    {
                        points.Add(new Point2D(posX + i, posY, i));

                        for (int j = 1; j < length; j++)
                        {
                            points.Add(new Point2D(posX + i, posY + j, (i < j) ? j : i));
                        }
                    }
                }
                else if (rotation == 2 || rotation == 6)
                {
                    for (int i = 1; i < width; i++)
                    {
                        points.Add(new Point2D(posX, posY + i, i));

                        for (int j = 1; j < length; j++)
                        {
                            points.Add(new Point2D(posX + j, posY + i, (i < j) ? j : i));
                        }
                    }
                }
            }

            return points;
        }
    }
}
