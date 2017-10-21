using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    public class PopularRoomsSearchMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int category = int.Parse(msgEvent.ReadString());

            if (category == -1) // Show top X rooms based on player amount, fill rest with empty rooms
            {
                client.SendComposer(new GuestRoomSearchResultComposer(1, category.ToString(), Engine.Locator.NavigatorController.GetTopRooms()));
            }
            else
            {
                client.SendComposer(new GuestRoomSearchResultComposer(1, category.ToString(), Engine.Locator.NavigatorController.GetRoomsInCategory(category)));
            }
        }
    }
}