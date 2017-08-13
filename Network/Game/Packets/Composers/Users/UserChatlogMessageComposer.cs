using AuroraEmu.Game.Rooms;

namespace AuroraEmu.Network.Game.Packets.Composers.Users
{
    public class UserChatlogMessageComposer : MessageComposer
    {
        public UserChatlogMessageComposer(Room room, string message, int userId)
            : base(536)
        {
        }
    }
}
