﻿using AuroraEmu.Game.Items;

namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    public class ObjectUpdateMessageComposer : MessageComposer
    {
        public ObjectUpdateMessageComposer(Item item)
            : base(95)
        {
            AppendVL64(item.Id);
            AppendVL64(item.DefinitionId);
            AppendVL64(item.X);
            AppendVL64(item.Y);
            AppendVL64(item.RoomId);
            AppendString(item.Z.ToString());
            AppendVL64(0);
            AppendString(item.Data);
            AppendVL64(-1);
        }
    }
}
