using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Messenger;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class MessengerInitMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            client.SendComposer(new MessengerInitMessageComposer(MessengerController.GetInstance().GetFriendsById(client.Player.Id)));
        }
    }
}
