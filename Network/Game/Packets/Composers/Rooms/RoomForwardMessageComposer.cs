using AuroraEmu.Game.Rooms.Models;

namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    public class RoomForwardMessageComposer : MessageComposer
    {
        public RoomForwardMessageComposer(Room room)
            : base(286)
        {
            AppendVL64(true);
            AppendVL64(room.Id);
        }
    }
}
