using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    class RoomTextSearchMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int history = msgEvent.ReadVL64();
            string search = msgEvent.ReadString();

            client.SendComposer(new GuestRoomSearchResultComposer(history, 9, search, Engine.MainDI.NavigatorController.SearchRooms(search)));
        }
    }
}
