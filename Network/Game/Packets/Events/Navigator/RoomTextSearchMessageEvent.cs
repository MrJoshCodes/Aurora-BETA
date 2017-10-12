using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    class RoomTextSearchMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            string search = msgEvent.ReadString();

            client.SendComposer(new GuestRoomSearchResultComposer(9, search, Engine.Locator.NavigatorController.SearchRooms(search)));
        }
    }
}
