using AuroraEmu.Game.Badges;
using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Inventory.Badges;
using AuroraEmu.Utilities;
using System;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Events.Inventory.Badges
{
    class SetActivatedBadgesEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.Player.BadgesComponent.ClearBadgeSlots();

            //Unregistered packet event #158: I@CADMJ@CEXHK@@PA@@QA@@
            List<(int, int)> list = new List<(int, int)>();

            while (msgEvent.HasBytes)
            {
                int slot = msgEvent.ReadVL64();
                string badge = msgEvent.ReadString();

                if (!string.IsNullOrEmpty(badge) && slot >= 1 && slot <= 5 && client.Player.BadgesComponent.TryGetBadge(badge, out Badge b))
                {
                    client.Player.BadgesComponent.Badges[b.Id].Slot = slot;

                    list.Add((b.Id, slot));
                }
            }

            client.Player.BadgesComponent.UpdateBadgeSlots(list);

            if (client.CurrentRoomId > 0)
            {
                Engine.MainDI.RoomController.GetRoom(client.CurrentRoomId).SendComposer(new HabboUserBadgesMessageComposer(client.Player.Id, client.Player.BadgesComponent.GetEquippedBadges()));
            }
            else
            {
                client.SendComposer(new HabboUserBadgesMessageComposer(client.Player.Id, client.Player.BadgesComponent.GetEquippedBadges()));
            }
        }
    }
}
