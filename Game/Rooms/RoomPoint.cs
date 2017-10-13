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
        private Item HighestItem =>
            Items[Items.Count - 1];

        public RoomPoint(int x, int y)
        {
            X = x;
            Y = y;
            Items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            double _totalHeight = 0;
            foreach (Item stackItem in Items)
            {
                if (stackItem.Position.Z > stackItem.Definition.Height)
                    _totalHeight += stackItem.Position.Z;
                else
                    _totalHeight += stackItem.Definition.Height;
            }
            item.Position.Z = _totalHeight;
            Items.Add(item);
            Items.OrderByDescending(x => x.Position.Z);
        }

        public void RotateItem(Item item)
        {
            if (item.Equals(HighestItem))
                return;
            double _totalHeight = 0;
            foreach (Item stackItem in Items)
            {
                _totalHeight += stackItem.Definition.Height + stackItem.Position.Z;
            }
            item.Position.Z = _totalHeight;
            Items.OrderByDescending(x => x.Position.Z);
        }

        public void Dispose()
        {
            Items.Clear();
        }
    }
}
