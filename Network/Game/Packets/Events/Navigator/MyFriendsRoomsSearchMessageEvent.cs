using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    public class MyFriendsRoomsSearchMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.SendComposer(new GuestRoomSearchResultComposer(4, "", Engine.MainDI.NavigatorController.GetRoomsByFriends(client.Player.Id)));
        }
    }
}