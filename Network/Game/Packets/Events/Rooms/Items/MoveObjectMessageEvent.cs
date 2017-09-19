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
                if(client.UserActor.Position.X == item.X && client.UserActor.Position.Y == item.Y)
                    if(item.Definition.ItemType == "seat")
                    {
                        if(item.Rotation != rotation)
                        {
                            client.UserActor.Rotation = rotation;
                            client.UserActor.UpdateNeeded = true;
                        }
                        else
                        {
                            client.UserActor.Statusses.Remove("sit");
                            client.UserActor.UpdateNeeded = true;
                        }
                    }
                item.X = x;
                item.Y = y;
                item.Rotation = rotation;
                client.CurrentRoom.SendComposer(new ObjectUpdateMessageComposer(item));
                Engine.MainDI.ItemDao.UpdateItem(itemId, x, y, rotation, client.CurrentRoom.Id);
            }
        }
    }
}
