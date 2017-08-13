using AuroraEmu.Game.Clients;

namespace AuroraEmu.Game.Rooms.User
{
    public class UserActor : RoomActor
    {
        public Client client { get; private set; }
        public int virtualId { get; private set; }

        public UserActor(Client client, int virtualId)
            : base(client, virtualId)
        {
            this.client = client;
            this.virtualId = virtualId;
        }
    }
}
