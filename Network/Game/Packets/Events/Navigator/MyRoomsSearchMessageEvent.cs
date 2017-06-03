using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Navigator;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    class MyRoomsSearchMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.SendComposer(new GuestRoomSearchResultComposer(msgEvent.ReadVL64(), 5, "", NavigatorController.GetInstance().GetRoomsByOwner(client.Player.Id)));
        }
    }
}
