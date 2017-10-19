using System.Collections.Generic;
using AuroraEmu.Game.Badges.Models;

namespace AuroraEmu.Network.Game.Packets.Composers.Users
{
    public class HabboUserBadgesMessageComposer : MessageComposer
    {
        public HabboUserBadgesMessageComposer(int playerId, ICollection<Badge> badges) : base(228)
        {
            AppendString(playerId.ToString());
            AppendVL64(badges.Count);

            foreach (Badge badge in badges)
            {
                AppendVL64(badge.Slot);
                AppendString(badge.Code);
            }
        }
    }
}