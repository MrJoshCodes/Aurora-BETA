using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    class MyRoomsSearchMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            if (client.FlashClient)
            {
                client.SendComposer(new GuestRoomSearchResultComposer(5, "", Engine.Locator.NavigatorController.GetRoomsByOwner(client.Player.Id)));
            }
            else
            {
                List<Room> rooms = Engine.Locator.NavigatorController.GetRoomsByOwner(client.Player.Id);

                if (rooms.Count > 0)
                {
                    client.SendComposer(new UserFlatResultMessageComposer(rooms));
                }
                else
                {
                    client.SendComposer(new NoFlatsForUserMessageComposer());
                }
            }
        }
    }
}
