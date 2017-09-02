using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    public class PickupObjectMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            if (client.CurrentRoomId < 1)
                return;

            Room room = Engine.MainDI.RoomController.GetRoom(client.CurrentRoomId);
            int junk = msg.ReadVL64();
            int itemId = msg.ReadVL64();
            
            if(room.Items.TryRemove(itemId, out Item item))
            {
                client.Items.Add(itemId, item);
                client.SendComposer(new ObjectRemoveMessageComposer(itemId));
            }
        }
    }
}
