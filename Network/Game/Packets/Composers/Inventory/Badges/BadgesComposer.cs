using AuroraEmu.Game.Badges.Models;
using System.Collections.Generic;
using System.Linq;

namespace AuroraEmu.Network.Game.Packets.Composers.Inventory.Badges
{
    class BadgesComposer : MessageComposer
    {
        public BadgesComposer(Dictionary<int, Badge> badges)
            : base(229)
        {
            IEnumerable<Badge> equippedBadges = badges.Values.Where(badge => badge.Slot > 0);

            AppendVL64(badges.Count);

            foreach (Badge badge in badges.Values)
            {
                AppendString(badge.Code);
            }

            AppendVL64(equippedBadges.Count());

            foreach(Badge equippedBadge in equippedBadges)
            {
                AppendVL64(equippedBadge.Slot);
                AppendString(equippedBadge.Code);
            }
        }
    }
}
