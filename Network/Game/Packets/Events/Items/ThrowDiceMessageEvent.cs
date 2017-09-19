using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using AuroraEmu.Network.Game.Packets.Composers.Items;

namespace AuroraEmu.Network.Game.Packets.Events.Items
{
    public class ThrowDiceMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int itemId = msgEvent.ReadVL64();

            if(client.CurrentRoom.Items.TryGetValue(itemId, out Item item))
            {
                if (item.Cycling)
                    return;
                item.Handler.Trigger(item, -1);
                client.SendComposer(new ObjectDataUpdateMessageComposer(itemId, item.Data));
                item.ProcessItem(client, 3);
            }
        }
    }
}
