using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Users;

namespace AuroraEmu.Network.Game.Packets.Events.Users
{
    class InfoRetrieveMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.SendComposer(new UserObjectComposer(client.Player));
        }
    }
}
