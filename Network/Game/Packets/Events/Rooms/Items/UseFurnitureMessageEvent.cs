using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items.Models;
using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.Network.Game.Packets.Composers.Items;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms.Items
{
    public class UseFurnitureMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int itemId = msgEvent.ReadVL64();
            Room room = Engine.Locator.RoomController.GetRoom(client.CurrentRoomId);

            if (room == null) return;
            if (!room.Items.TryGetValue(itemId, out Item item)) return;

            item.ProcessItem(client);
        }
    }
}