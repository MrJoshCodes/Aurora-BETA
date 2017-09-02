using AuroraEmu.Game.Badges;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuroraEmu.Network.Game.Packets.Composers.Inventory.Badges
{
    class HabboUserBadgesMessageComposer : MessageComposer
    {
        public HabboUserBadgesMessageComposer(int playerId, List<Badge> equippedBadges)
            : base(228)
        {
            base.AppendVL64(playerId);
            base.AppendVL64(equippedBadges.Count);

            foreach (Badge badge in equippedBadges)
            {
                base.AppendString(badge.Code);
                base.AppendVL64(badge.Slot);
            }
        }
    }
}
