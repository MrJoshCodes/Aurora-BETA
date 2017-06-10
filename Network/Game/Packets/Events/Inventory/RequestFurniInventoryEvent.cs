using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using AuroraEmu.Network.Game.Packets.Composers.Inventory;

namespace AuroraEmu.Network.Game.Packets.Events.Inventory
{
    class RequestFurniInventoryEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            if (client.Items == null)
                client.Items = ItemController.GetInstance().GetItemsFromOwner(client.Player.Id);

            // HH <- unk?

            client.SendComposer(new FurniListComposer(client.Items));
        }
    }
}
