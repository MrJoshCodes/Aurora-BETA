using AuroraEmu.Game.Rooms;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Navigator
{
    class UserFlatCatsComposer : MessageComposer
    {
        public UserFlatCatsComposer(List<RoomCategory> categories)
            : base(221)
        {
            AppendVL64(categories.Count);

            foreach(RoomCategory category in categories)
            {
                AppendVL64(category.Id);
                AppendString(category.Name);
            }
        }
    }
}
