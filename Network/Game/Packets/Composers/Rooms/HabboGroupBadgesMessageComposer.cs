using System.Collections.Generic;
using AuroraEmu.Game.Groups.Models;

namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class HabboGroupBadgesMessageComposer : MessageComposer
    {
        public HabboGroupBadgesMessageComposer(Dictionary<int, Group> groups)
            : base(309)
        {
            AppendVL64(groups.Count);

            foreach (var group in groups.Values)
            {
                AppendVL64(group.Id);
                AppendString(group.Badge);
            }
        }
    }
}
