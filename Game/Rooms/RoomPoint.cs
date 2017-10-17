using AuroraEmu.Game.Items.Models;
using AuroraEmu.Game.Rooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuroraEmu.Game.Rooms
{
    public class RoomPoint : IDisposable
    {
        public int X { get; }
        public int Y { get; }
        private double _highestHeight = 0d;
        public double TileHeight =>
            _highestHeight;
        public List<RoomActor> Actors { get; private set; }
        public List<Item> Items { get; private set; }
        public Item HighestItem =>
            Items.Count > 0 ? Items[0] : null;
        public Item ItemBeneath(Item item) =>
            Items.Count > 1 ? Items[Items.IndexOf(item) + 1] : null;

        public RoomPoint(int x, int y)
        {
            X = x;
            Y = y;
            Items = new List<Item>();
            Actors = new List<RoomActor>();
        }

        public void AddItem(Item item)
        {
            if (Items.Count > 0)
            {
                foreach (Item stackItem in Items)
                {
                    double totalHeight = stackItem.Position.Z;

                    if (totalHeight > _highestHeight)
                        _highestHeight = totalHeight;
                }
            }
            else
            {
                _highestHeight = 0;
            }

            item.Position.Z = _highestHeight;

            if (item.Definition.ApplyStackHeight)
                _highestHeight += item.Definition.Height;

            Items.Add(item);
            Items.OrderBy(itemToOrder => itemToOrder.Position.Z);
        }

        public void RemoveItem(Item item)
        {
            if (HighestItem != null && HighestItem.Equals(item))
            {
                if (item.Definition.ApplyStackHeight) 
                {
                    _highestHeight -= item.Definition.Height;
                }
            }

            Items.Remove(item);
        }

        public void RotateItem(Item item)
        {
            RemoveItem(item);
            AddItem(item);
        }

        public void Dispose()
        {
            Items.Clear();
        }
    }
}
