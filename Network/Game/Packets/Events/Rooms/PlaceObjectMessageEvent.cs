using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;
using System.Collections;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    public class PlaceObjectMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
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
                    }
                    else
                    {
                        int x = int.Parse(dataBits[1]);
                        int y = int.Parse(dataBits[2]);
                        int rot = int.Parse(dataBits[3]);
                        item.X = x;
                        item.Y = y;
                        item.Rotation = rot;
                        
                        client.Items.Remove(itemId);
                        client.CurrentRoom.Items.AddOrUpdate(itemId, item, (oldKey, newKey) => item);
                        Engine.MainDI.ItemController.AddFloorItem(itemId, x, y, rot, client.CurrentRoom.Id);
                        client.CurrentRoom.SendComposer(new ObjectAddMessageComposer(item));
                    }
                }
            }
        }
    }
}
