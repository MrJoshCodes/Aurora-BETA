using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Messenger;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class MessengerInitMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            if (client.Friends == null)
                client.Friends = MessengerController.GetInstance().GetFriendsById(client.Player.Id);

            client.SendComposer(new MessengerInitMessageComposer(client.Friends));
        }
    }
}
