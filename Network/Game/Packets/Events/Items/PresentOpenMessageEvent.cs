using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items.Models;
using AuroraEmu.Network.Game.Packets.Composers.Catalogue;
using AuroraEmu.Network.Game.Packets.Composers.Inventory;
using AuroraEmu.Network.Game.Packets.Composers.Items;

namespace AuroraEmu.Network.Game.Packets.Events.Items
{
    public class PresentOpenMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int presentId = msgEvent.ReadVL64();

            if (Engine.Locator.RoomController.GetRoom(client.CurrentRoomId).Items.TryGetValue(presentId, out Item item))
            {
                (int, string) presentData = Engine.Locator.ItemController.GetPresent(presentId, client.Player.Id);
                ItemDefinition definition = Engine.Locator.ItemController.GetTemplate(presentData.Item1);

                if (definition != null)
                {
                    if (client.CurrentRoom.Items.TryRemove(presentId, out Item tmp))
                    {
                        client.CurrentRoom.Grid.PickupObject(item);
                        Engine.Locator.ItemController.Dao.DeleteItem(item.Id);

                        client.QueueComposer(new ObjectRemoveMessageComposer(item.Id));
                        client.QueueComposer(new PresentOpenedMessageComposer(definition));

                        Engine.Locator.ItemController.DeletePresent(presentId);

                        Engine.Locator.ItemController.GiveItem(client, definition, presentData.Item2);
                        
                        if (client.Items != null)
                            client.QueueComposer(new FurniListUpdateComposer());

                        client.Flush();
                    }
                }
            }
        }
    }
}