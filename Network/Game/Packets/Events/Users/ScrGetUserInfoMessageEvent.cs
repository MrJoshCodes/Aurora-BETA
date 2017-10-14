using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Subscription.Models;
using AuroraEmu.Network.Game.Packets.Composers.Users;

namespace AuroraEmu.Network.Game.Packets.Events.Users
{
    public class ScrGetUserInfoMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            string subscriptionId = msgEvent.ReadString();

            if(client.SubscriptionData.TryGetValue(subscriptionId, out SubscriptionData data))
            {
                client.SendComposer(new ScrSendUserInfoComposer(data));
            }
        }
    }
}
