using AuroraEmu.Game.Rooms;
using System.Collections.Generic;
using System.Linq;

namespace AuroraEmu.Network.Game.Packets.Composers.Navigator
{
    class PopularRoomTagsResultComposer : MessageComposer
    {
        public PopularRoomTagsResultComposer(ICollection<RoomCategory> categories)
            :base(452)
        {
            AppendVL64(categories.Count);

            foreach (RoomCategory category in categories.OrderBy(x => x.PlayersInside))
            {
                AppendString(category.Name);
                AppendVL64(category.PlayersInside);
            }
        }
    }
}
