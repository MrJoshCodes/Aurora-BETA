using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms.Models;

namespace AuroraEmu.Game.Rooms.User
{
    public class UserActor : RoomActor
    {
        public Client UserClient;
        public int UserVirtualId;
        public override ActorType Type => ActorType.User;

        public UserActor(Client client, int virtualId)
            : base(client, virtualId)
        {
            UserClient = client;
            UserVirtualId = virtualId;
        }
    }
}
