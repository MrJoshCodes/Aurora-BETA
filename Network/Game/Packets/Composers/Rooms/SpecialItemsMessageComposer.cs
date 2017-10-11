using AuroraEmu.Game.Items.Models;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class SpecialItemsMessageComposer : MessageComposer
    {
        public SpecialItemsMessageComposer(List<Item> items)
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
            AppendVL64(item.Position.X);
            AppendVL64(item.Position.Y);
            AppendVL64((int)item.Position.Z);
            AppendVL64(item.Rotation);
        }
    }
}
