using AuroraEmu.Game.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class GetGuestRoomResultComposer : MessageComposer
    {
        public GetGuestRoomResultComposer(Room room)
            : base(454)
        {
            AppendVL64(true); // TODO: Find out what it does
            GuestRoomSearchResultComposer.SerializeRoom(room, this);
            AppendVL64(true); // TODO: Find out what it does
        }
    }
}
