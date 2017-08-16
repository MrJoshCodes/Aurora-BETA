using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    class CanCreateRoomMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            if (client.RoomCount == null)
                client.RoomCount = Engine.MainDI.RoomController.GetUserRoomCount(client.Player.Id);

            client.SendComposer(new CanCreateRoomComposer(client.RoomCount < Engine.MainDI.ConfigController.HHConfig.MaxRooms, Engine.MainDI.ConfigController.HHConfig.MaxRooms));
        }
    }
}
