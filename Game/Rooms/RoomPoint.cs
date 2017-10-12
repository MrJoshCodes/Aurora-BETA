using AuroraEmu.Game.Items.Models;
using System;
using System.Collections.Generic;

namespace AuroraEmu.Game.Rooms
{
    public class RoomPoint : IDisposable
    {
        public int X { get; }
        public int Y { get; }
        public List<Item> Items { get; private set; }

        public RoomPoint(int x, int y)
        {
            X = x;
            Y = y;
            Items = new List<Item>();
        }

        public void Dispose()
        {
            Items.Clear();
        }
    }
}
