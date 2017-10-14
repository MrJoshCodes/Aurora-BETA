using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class FriendListUpdateMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            client.SendComposer(new FriendListUpdateMessageComposer(client.Player.MessengerComponent.Friends));
        }
    }
}
