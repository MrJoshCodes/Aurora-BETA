using AuroraEmu.Game.Items;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Inventory
{
    class FurniListComposer : MessageComposer
    {
        public FurniListComposer(Dictionary<int, Item> items)
            : base(140)
        {
            AppendVL64(items.Count);

            foreach (Item item in items.Values)
            {
                SerializeItem(item);
            }
        }

        private void SerializeItem(Item item)
        {
            AppendVL64(item.Id);
            AppendVL64(0); // maybe page?
            AppendString(item.Definition.SpriteType.ToUpper());
            AppendVL64(item.Id);
            AppendVL64(item.Definition.SpriteId);
            AppendVL64(0); // TODO: Stickies, landscape, wallpaper
            AppendString(item.Data);
            AppendVL64(item.Definition.CanRecycle);
            AppendVL64(0); // TODO: Can trade
            AppendVL64(1); // Allow inventory stack
            AppendVL64(-1); // ?

            if (item.Definition.SpriteType.ToUpper().Equals("S"))
            {
                AppendString("");
                AppendVL64(-1);
            }
        }
    }
}
