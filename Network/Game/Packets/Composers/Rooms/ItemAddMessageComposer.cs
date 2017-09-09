using AuroraEmu.Game.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class ItemAddMessageComposer : MessageComposer
    {
        public ItemAddMessageComposer(Item item)
            : base(83)
        {
            AppendString(item.Id.ToString());
            AppendVL64(item.Definition.SpriteId);
            AppendString(item.Wallposition);
            AppendString(item.Data);
        }
    }
}
