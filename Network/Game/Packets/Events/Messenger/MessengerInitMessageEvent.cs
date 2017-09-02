using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;
using AuroraEmu.Network.Game.Packets.Composers.Users;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class MessengerInitMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            client.SendComposer(new MessengerInitMessageComposer(client.Player.MessengerComponent.Friends));
        }
    }
}