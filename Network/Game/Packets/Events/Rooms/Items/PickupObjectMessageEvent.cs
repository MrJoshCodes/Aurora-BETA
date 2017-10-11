using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Items;
using AuroraEmu.Game.Items.Models;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms.Items
{
    public class PickupObjectMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            int junk = msg.ReadVL64();
            int itemId = msg.ReadVL64();

            if (client.CurrentRoom.Items.TryRemove(itemId, out Item item))
            {
                client.CurrentRoom.Grid.PickupObject(item);
                client.Items.Add(itemId, item);
                client.QueueComposer(new FurniListUpdateComposer());
                client.QueueComposer(new ObjectRemoveMessageComposer(itemId));
                client.Flush();
                Engine.MainDI.ItemDao.UpdateItem(itemId, 0, 0, 0, null);
            }
        }
    }
}
