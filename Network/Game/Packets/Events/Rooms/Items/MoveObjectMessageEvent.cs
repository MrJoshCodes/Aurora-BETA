using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using AuroraEmu.Network.Game.Packets.Composers.Items;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms.Items
{
    public class MoveObjectMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int itemId = msgEvent.ReadVL64();
            int x = msgEvent.ReadVL64();
            int y = msgEvent.ReadVL64();
            int rotation = msgEvent.ReadVL64();

            if (client.CurrentRoom.Items.TryGetValue(itemId, out Item item))
            {
                if (item.Rotation == rotation)
                {
                    if (!client.CurrentRoom.Grid.MoveItem(item, rotation, x, y)) return;
                }
                else
                {
                    if (!client.CurrentRoom.Grid.RotateItem(item, rotation)) return;
                }
                item.Position.X = x;
                item.Position.Y = y;
                item.Rotation = rotation;
                client.CurrentRoom.SendComposer(new ObjectUpdateMessageComposer(item));
                Engine.MainDI.ItemDao.UpdateItem(itemId, x, y, rotation, client.CurrentRoom.Id);
            }
        }
    }
}
