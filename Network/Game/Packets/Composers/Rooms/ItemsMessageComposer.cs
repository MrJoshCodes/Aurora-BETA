﻿using AuroraEmu.Game.Items;
using System.Collections.Concurrent;

namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class ItemsMessageComposer : MessageComposer
    {
        public ItemsMessageComposer(ConcurrentBag<Item> wallItems)
            : base(45)
        {
            AppendVL64(wallItems.Count);

            foreach (Item wallItem in wallItems)
            {
                SerializeItem(wallItem);
            }
        }
        
        private void SerializeItem(Item item)
        {
            AppendString(item.Id.ToString());
            AppendVL64(item.Definition.SpriteId);
            AppendString(item.Wallposition);
            AppendString(item.Data);
        }
    }
}
