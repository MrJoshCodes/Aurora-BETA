using AuroraEmu.Game.Badges.Models;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Inventory.Badges
{
    class HabboUserBadgesMessageComposer : MessageComposer
    {
        public HabboUserBadgesMessageComposer(int playerId, List<Badge> equippedBadges)
            : base(228)
        {
            base.AppendString(playerId.ToString());
            base.AppendVL64(equippedBadges.Count);

            foreach (Badge badge in equippedBadges)
            {
                base.AppendVL64(badge.Slot);
                base.AppendString(badge.Code);
            }
        }
    }
}
