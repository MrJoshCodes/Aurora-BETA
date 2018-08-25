using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms.Items
{
    class GetItemsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            if (client.CurrentRoom == null)
                return;

            client.QueueComposer(new ItemsMessageComposer(client.CurrentRoom.GetWallItems()));
        }
    }
}
