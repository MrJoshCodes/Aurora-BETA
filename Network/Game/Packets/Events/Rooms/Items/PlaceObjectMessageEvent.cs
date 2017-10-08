using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using AuroraEmu.Network.Game.Packets.Composers.Items;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms.Items
{
    public class PlaceObjectMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            if (client.CurrentRoomId < 1)
                return;
            if (!client.CurrentRoom.UserRights.Contains(client.Player.Id) ||
                client.CurrentRoom.OwnerId != client.Player.Id) return;

            string placementData = msg.ReadString();
            string[] dataBits = placementData.Split(' ');
            int itemId = int.Parse(dataBits[0]);

            if (client.Items.TryGetValue(itemId, out Item item))
            {
                if (item != null)
                {
                    if (dataBits[1].StartsWith(":"))
                    {
                        string wallPosition = (dataBits[1] + " " + dataBits[2] + " " + dataBits[3]);

                        item.Wallposition = wallPosition;

                        client.Items.Remove(itemId);
                        client.CurrentRoom.Items.AddOrUpdate(itemId, item, (oldkey, newkey) => item);
                        Engine.MainDI.ItemController.AddWallItem(itemId, wallPosition, client.CurrentRoom.Id);
                        client.CurrentRoom.SendComposer(new ItemAddMessageComposer(item));
                        client.CurrentRoom.SendComposer(new ItemUpdateMessageComposer(item));
                    }
                    else
                    {
                        int x = int.Parse(dataBits[1]);
                        int y = int.Parse(dataBits[2]);
                        int rot = int.Parse(dataBits[3]);

                        if (!client.CurrentRoom.Grid.PlaceObject(x, y, rot, item)) return;

                        item.Position.X = x;
                        item.Position.Y = y;
                        item.Rotation = rot;

                        if (client.CurrentRoom.Items.TryAdd(itemId, item))
                        {
                            client.Items.Remove(itemId);
                            Engine.MainDI.ItemDao.UpdateItem(itemId, x, y, rot, client.CurrentRoom.Id);
                            client.SendComposer(new Composers.Rooms.FurniListUpdateComposer());
                            client.CurrentRoom.SendComposer(new ObjectAddMessageComposer(item));
                        }
                    }
                    client.SendComposer(new Composers.Inventory.FurniListUpdateComposer());
                }
            }
        }
    }
}
