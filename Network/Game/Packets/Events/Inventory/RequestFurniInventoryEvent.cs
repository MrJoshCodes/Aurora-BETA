using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Inventory;

namespace AuroraEmu.Network.Game.Packets.Events.Inventory
{
    class RequestFurniInventoryEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            if (client.Items != null)
                client.Items = Engine.MainDI.ItemController.GetItemsFromOwner(client.Player.Id);

            // HH <- unk?

            client.SendComposer(new FurniListComposer(client.Items));
        }
    }
}
