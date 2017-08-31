using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;
using AuroraEmu.Network.Game.Packets.Composers.Users;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class MessengerInitMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            client.QueueComposer(new CreditBalanceMessageComposer(client.Player.Coins));
            client.QueueComposer(new HabboActivityPointNotificationMessageComposer(client.Player.Pixels, 0));

            client.QueueComposer(new MessengerInitMessageComposer(client.Player.MessengerComponent.Friends));

            client.Flush();
        }
    }
}