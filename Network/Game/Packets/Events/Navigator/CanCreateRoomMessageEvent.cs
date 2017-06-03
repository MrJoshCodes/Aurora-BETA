using AuroraEmu.Config;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    class CanCreateRoomMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            if (client.RoomCount == null)
                client.RoomCount = RoomController.GetInstance().GetUserRoomCount(client.Player.Id);

            client.SendComposer(new CanCreateRoomComposer(client.RoomCount < ConfigLoader.GetInstance().HHConfig.MaxRooms, ConfigLoader.GetInstance().HHConfig.MaxRooms));
        }
    }
}
