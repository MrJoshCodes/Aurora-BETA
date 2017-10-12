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
        public bool[,] EntityGrid { get; }

        public RoomGrid(Room room)
        {
            _room = room;
            _tileAt = new Dictionary<(int, int), RoomPoint>();
            for (int x = 0; x < _room.Map.MapSize.Item1; x++)
            {
                for (int y = 0; y < _room.Map.MapSize.Item2; y++)
                {
                    _tileAt.Add((x, y), new RoomPoint(x, y));
                }
            }

            foreach (Item item in _room.Items.Values.Where(x => (x.Position.X != 0 && x.Position.Y != 0)))
            {
                foreach (Point2D tile in item.Tiles())
                {
                    _tileAt[(tile.X, tile.Y)].Items.Add(item);
                }
            }
            EntityGrid = new bool[_room.Map.MapSize.Item1, _room.Map.MapSize.Item2];
        }

        /// <summary>
        /// Removes the object from the dictionary
        /// Even the affected tiles will be removed.
        /// </summary>
        /// <param name="item">The object</param>
        public void PickupObject(Item item)
        {
            foreach (Point2D point in item.Tiles())
            {
                _tileAt[(point.X, point.Y)].Items.Remove(item);
            }
        }

        /// <summary>
        /// Tries to place the object to the roomgrid.
        /// </summary>
        /// <param name="x">The X-Coordination</param>
        /// <param name="y">The Y-Coordination</param>
        /// <param name="rot">The rotation state</param>
        /// <param name="item">The object</param>
        /// <returns>true if it's able to place else false</returns>
        public bool PlaceObject(int x, int y, int rot, Item item)
        {
            if (ItemsAt(x, y).Count > 0)
                return false;
            if (EntityGrid[x, y])
                return false;

            if (item.Definition.Length > 1 || item.Definition.Width > 1)
            {
                foreach (Point2D point in Utilities.Extensions.AffectedTiles(item.Definition.Length, item.Definition.Width, x, y, rot))
                {
                    if (ItemsAt(point).Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        _tileAt[(point.X, point.Y)].Items.Add(item);
                    }
                }
            }
            _tileAt[(x, y)].Items.Add(item);
            return true;
        }

        /// <summary>
        /// Tries to move the object on the roomgrid.
        /// </summary>
        /// <param name="item">The object</param>
        /// <param name="rot">The rotation-state</param>
        /// <param name="newX">The new X-Coordination</param>
        /// <param name="newY">The new Y-Coordination</param>
        /// <returns>True if it succeeded else false</returns>
        public bool MoveItem(Item item, int rot, int newX, int newY)
        {
            if (ItemsAt(newX, newY).Count > 0)
                return false;
            if (EntityGrid[newX, newY])
                return false;

            if (item.Definition.Length > 1 || item.Definition.Width > 1)
            {
                var affectedTiles = Utilities.Extensions.AffectedTiles(item.Definition.Length, item.Definition.Width, newX, newY, rot);
                foreach (Point2D point in affectedTiles)
                {
                    if (ItemsAt(point).Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        _tileAt[(point.X, point.Y)].Items.Add(item);
                    }
                }

                foreach (Point2D point in item.AffectedTiles)
                    _tileAt[(point.X, point.Y)].Items.Remove(item);
            }

            _tileAt[(item.Position.X, item.Position.Y)].Items.Remove(item);
            _tileAt[(newX, newY)].Items.Add(item);
            return true;
        }

        /// <summary>
        /// Tries to rotate the objects, this is for wider objects, so they don't collide
        /// </summary>
        /// <param name="item">The object</param>
        /// <param name="rot">The new rotation</param>
        /// <returns>True if it succeeded else false</returns>
        public bool RotateItem(Item item, int rot)
        {
            if (item.Definition.Length > 1 || item.Definition.Width > 1)
            {
                var affectedTiles = Utilities.Extensions.AffectedTiles(item.Definition.Length, item.Definition.Width, item.Position.X, item.Position.Y, rot);
                foreach (Point2D point in affectedTiles)
                {
                    if (ItemsAt(point.X, point.Y).Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        _tileAt[(point.X, point.Y)].Items.Add(item);
                    }
                }

                foreach (Point2D point in item.AffectedTiles)
                    _tileAt[(point.X, point.Y)].Items.Remove(item);
            }
            return true;
        }

        /// <summary>
        /// Checks if the step is valid.
        /// </summary>
        /// <param name="x">The X-Coordination</param>
        /// <param name="y">The Y-Coordination</param>
        /// <param name="actor">The room actor</param>
        /// <returns>True if the step was valid else false</returns>
        public bool ValidStep(int x, int y, RoomActor actor)
        {
            if (!_room.Map.PassableTiles[x, y])
                return false;

            if (EntityGrid[x, y])
                return false;


            if (ItemsAt(x, y).Count > 0)
            {
                Item item = ItemsAt(x, y)[0];

                if (actor.TargetPoint.X == x && actor.TargetPoint.Y == y)
                    if (item.Definition.ItemType == "seat")
                        return true;

                if (item.Definition.ItemType == "solid" ||
                    item.Definition.ItemType == "trophy" ||
                    item.Definition.ItemType == "seat")
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the item at a specific tile, instead of looping through all items
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

        public void Dispose()
        {
            foreach (RoomPoint point in _tileAt.Values)
                point.Dispose();
            _tileAt.Clear();
            _tileAt = null;
        }
    }
}