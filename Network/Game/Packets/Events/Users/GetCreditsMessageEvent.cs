using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Users;

namespace AuroraEmu.Network.Game.Packets.Events.Users
{
    public class GetCreditsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.QueueComposer(new CreditBalanceMessageComposer(client.Player.Coins));
            client.QueueComposer(new HabboActivityPointNotificationMessageComposer(client.Player.Pixels, 0));
            client.Flush();
        }
    }
}
