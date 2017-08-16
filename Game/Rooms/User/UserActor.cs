using AuroraEmu.Game.Clients;

namespace AuroraEmu.Game.Rooms.User
{
    public class UserActor : RoomActor
    {
        public Client UserClient;
        public int UserVirtualId;

        public UserActor(Client client, int virtualId)
            : base(client, virtualId)
        {
            UserClient = client;
            UserVirtualId = virtualId;
        }
    }
}
