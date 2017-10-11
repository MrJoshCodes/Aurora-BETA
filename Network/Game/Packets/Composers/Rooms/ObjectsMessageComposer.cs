using AuroraEmu.Game.Items.Models;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class ObjectsMessageComposer : MessageComposer
    {
        public ObjectsMessageComposer(List<Item> floorItems)
            : base (32)
        {
            AppendVL64(floorItems.Count);

            foreach (Item floorItem in floorItems)
            {
                SerializeItem(floorItem);
            }
        }

        private void SerializeItem(Item item)
        {
            AppendVL64(item.Id);
            AppendVL64(item.Definition.SpriteId);
            AppendVL64(item.Position.X);
            AppendVL64(item.Position.Y);
            AppendVL64(item.Rotation);
            AppendString(item.Position.Z.ToString());
            AppendVL64(0); // Not used AT ALL
            AppendString(item.Data);
            AppendVL64(-1); // Not used AT ALL
        }
    }
}
