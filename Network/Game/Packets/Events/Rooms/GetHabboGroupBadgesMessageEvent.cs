using System.Collections.Generic;
using System.Linq;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Groups.Models;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class GetHabboGroupBadgesMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            var room = Engine.Locator.RoomController.GetRoom(client.LoadingRoomId);

            if (room == null) return;

            var currentGroupBadges = (from actor in room.Actors.Values
                where actor.Client.Player.Group != null
                group actor by actor.Client.Player.Group.Id
                into g
                select g.First().Client.Player.Group).ToDictionary(group => group.Id);

            if (client.Player.Group != null && !currentGroupBadges.ContainsKey(client.Player.Group.Id))
                currentGroupBadges.Add(client.Player.Group.Id, client.Player.Group);
                                                                            
            
            client.SendComposer(new HabboGroupBadgesMessageComposer(currentGroupBadges));
        }
    }
}
