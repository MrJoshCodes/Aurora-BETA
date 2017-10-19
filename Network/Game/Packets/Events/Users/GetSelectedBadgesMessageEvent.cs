using System.Linq;
using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Inventory.Badges;

namespace AuroraEmu.Network.Game.Packets.Events.Users
{
    public class GetSelectedBadgesMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int playerId = msgEvent.ReadVL64();
            Client target = Engine.Locator.ClientController.GetClientByHabbo(playerId);

            if (target != null && target.Player.BadgesComponent != null)
            {
                client.SendComposer(new HabboUserBadgesMessageComposer(playerId, target.Player.BadgesComponent.GetEquippedBadges()));
            }
            else
            {
                client.SendComposer(new HabboUserBadgesMessageComposer(playerId, Engine.Locator.BadgeController.Dao.GetBadges(playerId).Values.Where(badge => badge.Slot > 0).ToList()));
            }
        }
    }
}