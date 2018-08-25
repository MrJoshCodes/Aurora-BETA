using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms.Items
{
    class GetObjectsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            if (client.CurrentRoom == null)
                return;

            if (!string.IsNullOrEmpty(client.CurrentRoom.CCTs))
            {

            }
            else
            {
                client.QueueComposer(new SpecialItemsMessageComposer(client.CurrentRoom.GetItems()));
                client.QueueComposer(new ObjectsMessageComposer(client.CurrentRoom.GetFloorItems()));
            }
        }
    }
}
