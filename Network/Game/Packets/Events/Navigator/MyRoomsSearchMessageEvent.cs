using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    class MyRoomsSearchMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.SendComposer(new GuestRoomSearchResultComposer(msgEvent.ReadVL64(), 5, "", Engine.MainDI.NavigatorController.GetRoomsByOwner(client.Player.Id)));
        }
    }
}
