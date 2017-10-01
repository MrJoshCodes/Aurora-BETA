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

            if (client.CurrentRoom.Items.TryGetValue(itemId, out Item item))
            {
                if (rotation == item.Rotation)
                    if (client.CurrentRoom.BlockedTiles[x, y])
                        return;
                foreach (Point2D point in item.Tiles)
                    client.CurrentRoom.BlockedTiles[point.X, point.Y] = false;
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

                var affectedTiles = Utilities.Extensions.AffectedTiles(item.Definition.Length, item.Definition.Width, x, y, rotation);
                affectedTiles.Add(new Point2D(x, y));
                foreach (Point2D point in affectedTiles)
                    client.CurrentRoom.BlockedTiles[point.X, point.Y] = true;

                client.CurrentRoom.SendComposer(new ObjectUpdateMessageComposer(item));
                Engine.MainDI.ItemDao.UpdateItem(itemId, x, y, rotation, client.CurrentRoom.Id);
            }
        }
    }
}
