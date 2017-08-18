using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Inventory.Badges;

namespace AuroraEmu.Network.Game.Packets.Events.Inventory.Badges
{
    class GetBadgesEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            if (client.Player.BadgesComponent != null)
            {
                client.SendComposer(new BadgesComposer(client.Player.BadgesComponent.Badges));
            }
        }
    }
}
