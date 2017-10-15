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
        private double highestHeight = 0d;

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
                    if (totalHeight > highestHeight)
                    {
                        highestHeight = totalHeight;
                    }
                }
            else
            {
                highestHeight = 0;
            }

            item.Position.Z = highestHeight;
            highestHeight += item.Definition.Height;
            Items.Add(item);
            Items.OrderByDescending(x => x.Position.Z);
        }

        public void RotateItem(Item item)
        {
            if (item.Equals(HighestItem))
                return;

            item.Position.Z = highestHeight;
            highestHeight += item.Definition.Height;
            Items.OrderByDescending(x => x.Position.Z);
        }

        public void Dispose()
        {
            Items.Clear();
        }
    }
}
