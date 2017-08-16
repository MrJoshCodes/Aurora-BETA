using AuroraEmu.Game.Clients;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class FriendListUpdateMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            client.SendComposer(client.Player.MessengerComponent.UpdateFriendList());
        }
    }
}
