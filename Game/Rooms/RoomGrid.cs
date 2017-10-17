using AuroraEmu.Game.Items.Models;
using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.Game.Rooms.Pathfinder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuroraEmu.Game.Rooms
{
    public class RoomGrid : IDisposable
    {
        private Room _room;
        private Dictionary<(int, int), RoomPoint> _tileAt;
        //public bool[,] EntityGrid { get; }

        public RoomGrid(Room room)
        {
            _room = room;

            //Initialize the grid
            _tileAt = new Dictionary<(int, int), RoomPoint>();
            for (int x = 0; x < _room.Map.MapSize.X; x++)
            {
                for (int y = 0; y < _room.Map.MapSize.Y; y++)
                {
                    _tileAt.Add((x, y), new RoomPoint(x, y));
                }
            }

            //Add the items to the grid
            foreach (Item item in _room.Items.Values.Where(x => (x.Position.X != 0 && x.Position.Y != 0)))
            {
                foreach (Point2D tile in item.Tiles)
                {
                    _tileAt[(tile.X, tile.Y)].AddItem(item);
                }
            }
            //EntityGrid = new bool[_room.Map.MapSize.X + 1, _room.Map.MapSize.Y + 1];
        }
        
        /// <summary>
        /// Removes the item from the list
        /// </summary>
        /// <param name="item">Rememoves the affected item</param>
        public void PickupObject(Item item)
        {
            foreach (Point2D point in item.Tiles)
            {
                _tileAt[(point.X, point.Y)].Items.Remove(item);
            }
        }

        /// <summary>
        /// Adds the item to the list
        /// </summary>
        /// <param name="points">The tiles the item is affecting</param>
        /// <param name="item">The item</param>
        public void PlaceObject(List<Point2D> points, Item item)
        {
            //Adds the item to the list
            foreach (Point2D point in points)
            {
                _tileAt[(point.X, point.Y)].AddItem(item);
            }
        }

        /// <summary>
        /// Moves the item
        /// </summary>
        /// <param name="item">The item</param>
        /// <param name="points">The tiles the item is affecting</param>
        public void MoveItem(Item item, List<Point2D> points)
        {
            //Removes the old affected tiles
            foreach (Point2D point in item.Tiles)
            {
                _tileAt[(point.X, point.Y)].RemoveItem(item);
            }

            //Adds the item to the new tiles
            foreach (Point2D point in points)
            {
                _tileAt[(point.X, point.Y)].AddItem(item);
            }
        }

        /// <summary>
        /// Rotates the item
        /// </summary>
        /// <param name="item">The item</param>
        /// <param name="rot">The new rotation</param>
        /// <param name="affTiles">The affected tiles</param>
        public void RotateItem(Item item, int rot, List<Point2D> affTiles)
        {
            if (item.Definition.Length > 1 || item.Definition.Width > 1)
            {
                //Adds the item to the affected tiles
                foreach (Point2D point in affTiles)
                {
                    _tileAt[(point.X, point.Y)].Items.Add(item);

                }

                //REmoves the item from the old affected tiles
                foreach (Point2D point in item.AffectedTiles)
                {
                    _tileAt[(point.X, point.Y)].Items.Remove(item);
                }
            }

            //Rotates the item
            _tileAt[(item.Position.X, item.Position.Y)].RotateItem(item);
        }

        //Simplify the ValidPoint method
        public bool ValidPoint(Point2D point) =>
            ValidPoint(point.X, point.Y);

        //Checks if the point is valid
        public bool ValidPoint(int x, int y)
        {
            if (!_room.Map.PassableTiles[x, y])
                return false;

            if (PointAt(x, y).Actors.Count > 0)
                return false;

            if (ItemsAt(x, y).Count > 0)
                if (_tileAt[(x, y)].HighestItem.Definition.CanStack || _tileAt[(x, y)].HighestItem.Definition.InteractorType == "roller")
                    return true;
                else
                    return false;

            return true;
        }


        /// <summary>
        /// Checks if the step is valid, used by pathfinder
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="actor"></param>
        /// <returns></returns>
        public bool ValidStep(int x, int y, RoomActor actor, bool retry)
        {
            if (!_room.Map.PassableTiles[x, y])
                return false;

            if (PointAt(x, y).Actors.Count > 0 && !PointAt(x, y).Actors.Contains(actor))
                return false;


            if (ItemsAt(x, y).Count > 0)
            {
                Item item = _tileAt[(x, y)].HighestItem;

                if (actor.TargetPoint.X == x && actor.TargetPoint.Y == y)
                    if (item.Definition.ItemType == "seat")
                        return true;

                if (retry)
                {
                    if (item.Definition.ItemType == "solid" ||
                        item.Definition.ItemType == "trophy")
                        return false;
                }
                else
                {
                    if (item.Definition.ItemType == "solid" ||
                        item.Definition.ItemType == "trophy" ||
                        item.Definition.ItemType == "seat")
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the items at a specific point, instead of looping through all items
        /// and checking by value.
        /// </summary>
        /// <param name="point">The Point (Coordination)</param>
        /// <returns>The item if it exists else null</returns>
        public List<Item> ItemsAt(Point2D point)
        {
            return _tileAt[(point.X, point.Y)].Items;
        }

        public List<Item> ItemsAt(int x, int y)
        {
            return _tileAt[(x, y)].Items;
        }

        public RoomPoint PointAt(int x, int y) 
        {
            return _tileAt[(x, y)];
        }

        public double TileHeight(Point2D point) =>
            _tileAt[(point.X, point.Y)].TileHeight + _room.Map.TileHeights[point.X, point.Y];

        /// <summary>
        /// Dispose the points
        /// </summary>
        public void Dispose()
        {
            foreach (RoomPoint point in _tileAt.Values)
                point.Dispose();
            _tileAt.Clear();
            _tileAt = null;
        }
    }
}