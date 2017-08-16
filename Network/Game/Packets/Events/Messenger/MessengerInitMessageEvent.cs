using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class MessengerInitMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            client.SendComposer(new MessengerInitMessageComposer(client.Player.MessengerComponent.Friends));
            client.SendComposer(new BuddyRequestsMessageComposer(client.Player.MessengerComponent.Requests));
        }
    }
}