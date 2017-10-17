using AuroraEmu.Game.Items.Models;
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
            Items[Items.Count - 1];
        private double _highestHeight = 0d;
        public double TileHeight =>
            _highestHeight;

        public RoomPoint(int x, int y)
        {
            X = x;
            Y = y;
            Items = new List<Item>();
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
            if (HighestItem.Equals(item))
            {
                _highestHeight -= item.Definition.Height;
            }
        }

        public void RotateItem(Item item)
        {
            if (item.Equals(HighestItem))
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
