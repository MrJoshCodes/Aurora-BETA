using AuroraEmu.Game.Rooms.Models;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Navigator
{
    class UserFlatResultMessageComposer : MessageComposer
    {
        public UserFlatResultMessageComposer(List<Room> rooms)
            : base(16)
        {
            AppendVL64(rooms.Count);

            foreach (Room room in rooms)
            {
                AppendVL64(room.Id);
                AppendString(room.Name);
                AppendString(room.Owner);
                AppendString(room.State.ToString());
                AppendVL64(room.PlayersIn);
                AppendVL64(room.PlayersMax);
                AppendString(room.Description);
            }
        }
    }
}
