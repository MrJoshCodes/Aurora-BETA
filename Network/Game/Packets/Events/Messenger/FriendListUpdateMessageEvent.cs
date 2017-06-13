using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Messenger;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class FriendListUpdateMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            client.SendComposer(MessengerController.GetInstance().UpdateFriendlist(client.Player.Id));
        }
    }
}
