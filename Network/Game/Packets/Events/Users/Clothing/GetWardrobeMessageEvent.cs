using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Users.Clothing;

namespace AuroraEmu.Network.Game.Packets.Events.Users.Clothing
{
    public class GetWardrobeMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.SendComposer(new WardrobeMessageComposer(client));
        }
    }
}
