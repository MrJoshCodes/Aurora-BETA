using AuroraEmu.Game.Rooms.Models;

namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    public class UserRemoveMessageComposer : MessageComposer
    {
        public UserRemoveMessageComposer(RoomActor actor) 
            : base(29)
        {
            AppendString(actor.VirtualId.ToString());
        }
    }
}
