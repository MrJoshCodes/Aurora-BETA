using AuroraEmu.Game.Rooms;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Navigator
{
    class GuestRoomSearchResultComposer : MessageComposer
    {
        public GuestRoomSearchResultComposer(int tab1, int tab2, string search, List<Room> rooms)
            : base(451)
        {
            AppendVL64(tab1);
            AppendVL64(tab2);
            AppendString(search);
            AppendVL64(rooms.Count);

            foreach(Room room in rooms)
            {
                SerializeRoom(room);
            }
        }

        public void SerializeRoom(Room room)
        {
            AppendVL64(room.Id);
            AppendVL64(false); // events
            AppendString(room.Name);
            AppendString(room.Owner);
            AppendVL64((int)room.State);
            AppendVL64(7);
            AppendVL64(0);
            AppendString(room.Description);
            AppendVL64(0);
            AppendVL64(false); // can trade
            AppendVL64(0); // score
            AppendVL64(0); // tags
            AppendString(room.Icon, 0);
        }
    }
}
