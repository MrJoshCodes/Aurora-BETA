using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    public class UpdateNavigatorSettingsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            var homeRoom = msgEvent.ReadVL64();

            if (homeRoom != 0)
            {
                if (Engine.Locator.RoomController.GetRoom(homeRoom) == null) return;
            }

            client.Player.HomeRoom = homeRoom;
            Engine.Locator.PlayerController.Dao.UpdateHomeRoom(client.Player.Id, homeRoom);
            
            client.SendComposer(new NavigatorSettingsComposer(homeRoom));
        }
    }
}