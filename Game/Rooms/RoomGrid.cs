using AuroraEmu.Game.Items;
using AuroraEmu.Game.Rooms.Pathfinder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuroraEmu.Game.Rooms
{
    public class RoomGrid : IDisposable
    {
        private Room _room;
        private Dictionary<(int, int), Item> _itemsAt;
        public bool[,] EntityGrid { get; }

        public RoomGrid(Room room)
        {
            _room = room;
            _itemsAt = new Dictionary<(int, int), Item>();
            foreach (Item item in _room.GetFloorItems().Where(x => (x.Position.X != 0 && x.Position.Y != 0)))
            {
                if (item.Definition.Width > 1 || item.Definition.Length > 1)
                    foreach (Point2D tile in item.AffectedTiles)
                        _itemsAt.Add((tile.X, tile.Y), item);

                _itemsAt.Add((item.Position.X, item.Position.Y), item);
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
            if (item.Definition.Length > 1 || item.Definition.Width > 1)
                foreach (Point2D point in item.AffectedTiles)
                    _itemsAt.Remove((point.X, point.Y));
            _itemsAt.Remove((item.Position.X, item.Position.Y));
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
            if (ItemAt(x, y) != null)
                return false;
            if (EntityGrid[x, y])
                return false;

            if (item.Definition.Length > 1 || item.Definition.Width > 1)
            {
                foreach (Point2D point in Utilities.Extensions.AffectedTiles(item.Definition.Length, item.Definition.Width, x, y, rot))
                    if (ItemAt(point) != null)
                        return false;
                    else
                        _itemsAt.Add((point.X, point.Y), item);
            }
            _itemsAt.Add((x, y), item);
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
            if (ItemAt(newX, newY) != null)
                return false;
            if (EntityGrid[newX, newY])
                return false;

            if (item.Definition.Length > 1 || item.Definition.Width > 1)
            {
                var affectedTiles = Utilities.Extensions.AffectedTiles(item.Definition.Length, item.Definition.Width, newX, newY, rot);
                foreach (Point2D point in affectedTiles)
                    if (ItemAt(point) != null)
                        return false;
                    else
                        _itemsAt.Add((point.X, point.Y), item);

                foreach (Point2D point in item.AffectedTiles)
                    _itemsAt.Remove((point.X, point.Y));
            }
            _itemsAt.Remove((item.Position.X, item.Position.Y));
            _itemsAt.Add((newX, newY), item);
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
                    if (ItemAt(point.X, point.Y) != null)
                        return false;
                    else
                        _itemsAt.Add((point.X, point.Y), item);

                foreach (Point2D point in item.AffectedTiles)
                    _itemsAt.Remove((point.X, point.Y));
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

            if (_itemsAt.TryGetValue((x, y), out Item item))
            {
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
        /// I use native method TryGetValue();
        /// </summary>
        /// <param name="point">The Point (Coordination)</param>
        /// <returns>The item if it exists else null</returns>
        public Item ItemAt(Point2D point)
        {
            if (_itemsAt.TryGetValue((point.X, point.Y), out Item item))
                return item;
            return null;
        }

        public Item ItemAt(int x, int y)
        {
            if (_itemsAt.TryGetValue((x, y), out Item item))
                return item;
            return null;
        }

        public void Dispose()
        {
            _itemsAt.Clear();
            _itemsAt = null;
        }
    }
}