using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using AuroraEmu.Network.Game.Packets.Composers.Items;

namespace AuroraEmu.Network.Game.Packets.Events.Items
{
    public class SpinWheelOfFortuneMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int itemId = msgEvent.ReadVL64();

            if (client.CurrentRoom.Items.TryGetValue(itemId, out Item item))
            {
                if (item.Cycling)
                    return;
                item.Handler.Trigger(item);
                client.CurrentRoom.SendComposer(new ItemUpdateMessageComposer(item));
                item.ProcessItem(client, 8);
            }
        }
    }
}
