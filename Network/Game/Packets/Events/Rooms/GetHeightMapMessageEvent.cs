using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class GetHeightMapMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            if (client.LoadingRoomId < 1)
                return;

            Room room = Engine.MainDI.RoomController.GetRoom(client.LoadingRoomId);
            client.QueueComposer(new HeightMapMessageComposer(room.Map.RawMap));
            client.QueueComposer(new FloorHeightMapMessageComposer(room.Map.RelativeHeightMap));
            client.Flush();
        }
    }
}
