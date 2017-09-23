using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using AuroraEmu.Game.Rooms.Pathfinder;
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
            if (client.CurrentRoom.BlockedTiles[x, y])
                return;
            if (client.CurrentRoom.Items.TryGetValue(itemId, out Item item))
            {
                client.CurrentRoom.BlockedTiles[item.X, item.Y] = false;
                client.CurrentRoom.BlockedTiles[x, y] = true;
                item.X = x;
                item.Y = y;
                item.Rotation = rotation;
                if (item.ActorOnItem != null)
                    if (item.Definition.ItemType == "seat")
                    {
                        if (item.Rotation != rotation)
                        {
                            item.ActorOnItem.Rotation = rotation;
                            item.ActorOnItem.UpdateNeeded = true;
                        }
                        else
                        {
                            item.ActorOnItem.Statusses.Remove("sit");
                            item.ActorOnItem.UpdateNeeded = true;
                            item.ActorOnItem = null;
                        }
                    }

                client.CurrentRoom.SendComposer(new ObjectUpdateMessageComposer(item));
                Engine.MainDI.ItemDao.UpdateItem(itemId, x, y, rotation, client.CurrentRoom.Id);
            }
        }
    }
}
