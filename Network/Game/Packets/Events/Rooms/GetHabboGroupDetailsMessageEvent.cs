using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    public class GetHabboGroupDetailsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            var group = Engine.Locator.GroupController.Dao.GetGroup(msgEvent.ReadVL64());

            if (group == null) return;
            
            client.SendComposer(new HabboGroupDetailsMessageComposer(group));
        }
    }
}