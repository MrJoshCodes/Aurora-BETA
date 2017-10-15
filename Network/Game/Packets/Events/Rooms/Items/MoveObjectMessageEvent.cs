using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items.Models;
using AuroraEmu.Game.Rooms.Pathfinder;
using AuroraEmu.Network.Game.Packets.Composers.Items;
using System.Collections.Generic;

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
                //Gets the affected tiles and checks if the point is valid
                List<Point2D> points = Utilities.Extensions.AffectedTiles(item.Definition.Length, item.Definition.Width, x, y, rotation);
                foreach (Point2D point in points)
                    if (!client.CurrentRoom.Grid.ValidPoint(point))
                        return;

                if (item.Rotation == rotation)
                {
                    if (!client.CurrentRoom.Grid.ValidPoint(x, y))
                        return;

                    points.Add(new Point2D(x, y));
                    client.CurrentRoom.Grid.MoveItem(item, points);
                    
                }
                else
                {
                    client.CurrentRoom.Grid.RotateItem(item, rotation, points);
                }
                item.Position.X = x;
                item.Position.Y = y;
                item.Rotation = rotation;
                client.CurrentRoom.SendComposer(new ObjectUpdateMessageComposer(item));
                Engine.Locator.ItemController.Dao.UpdateItem(itemId, x, y, rotation, client.CurrentRoom.Id);
            }
        }
    }
}
