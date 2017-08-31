using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    public class PickupObjectMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            int junk = msg.ReadVL64();
            int itemId = msg.ReadVL64();
            
            if(client.CurrentRoom.Items.TryRemove(itemId, out Item item))
            {
                client.Items.Add(itemId, item);
                client.SendComposer(new ObjectRemoveMessageComposer(itemId));
            }
        }
    }
}
