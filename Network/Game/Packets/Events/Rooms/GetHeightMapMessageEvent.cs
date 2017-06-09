using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class GetHeightMapMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.QueueComposer(new HeightMapMessageComposer(client.LoadingRoom.Map.RawMap));
            client.QueueComposer(new FloorHeightMapMessageComposer(client.LoadingRoom.Map.RelativeHeightMap));
            client.Flush();
        }
    }
}
