using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    class GetFlatInfoMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int flatId = msgEvent.ReadVL64();

            Room room = Engine.Locator.RoomController.GetRoom(flatId);

            if (room == null)
            {
                return;
            }

            client.SendComposer(new FlatInfoMessageComposer(room));
        }
    }
}
