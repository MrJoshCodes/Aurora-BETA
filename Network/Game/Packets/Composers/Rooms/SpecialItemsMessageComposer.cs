using AuroraEmu.Game.Items;
using System.Collections.Concurrent;

namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class SpecialItemsMessageComposer : MessageComposer
    {
        public SpecialItemsMessageComposer(ConcurrentBag<Item> items)
            : base(30)
        {
            AppendVL64(items.Count - 1);

            foreach (Item item in items)
            {
                SerializeItem(item);
            }
        }
        
        private void SerializeItem(Item item)
        {
            AppendVL64(false);
            AppendString(item.Data);
            AppendString(item.Definition.SwfName);
            AppendVL64(item.X);
            AppendVL64(item.Y);
            AppendVL64((int)item.Z);
            AppendVL64(item.Rotation);
        }
    }
}
