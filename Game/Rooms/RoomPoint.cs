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
        public List<Item> Items { get; private set; }
        public Item HighestItem =>
            Items.Count > 0 ? Items[Items.Count - 1] : null;

        private double _highestHeight = 0d;
        public double TileHeight =>
            _highestHeight;
        
        public List<RoomActor> Actors { get; private set; }

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
                foreach (Item stackItem in Items)
                {
                    double totalHeight = stackItem.Position.Z;
                    if (totalHeight > _highestHeight)
                    {
                        _highestHeight = totalHeight;
                    }
                }
            else
            {
                _highestHeight = 0;
            }

            item.Position.Z = _highestHeight;
            _highestHeight += item.Definition.Height;
            Items.Add(item);
            Items.OrderByDescending(itemToOrder => itemToOrder.Position.Z);
        }

        public void RemoveItem(Item item)
        {
            if (HighestItem != null && HighestItem.Equals(item))
            {
                _highestHeight -= item.Definition.Height;
            }

            Items.Remove(item);
        }

        public void RotateItem(Item item)
        {
            if (HighestItem != null && HighestItem.Equals(item))
                return;

            item.Position.Z = _highestHeight;
            _highestHeight += item.Definition.Height;
            Items.OrderByDescending(itemToOrder => itemToOrder.Position.Z);
            Console.WriteLine(HighestItem.Definition.SwfName);
        }

        public void Dispose()
        {
            Items.Clear();
        }
    }
}
