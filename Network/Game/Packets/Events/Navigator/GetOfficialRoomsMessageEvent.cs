using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Navigator;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    class GetOfficialRoomsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.SendComposer(new OfficialRoomsComposer(NavigatorController.GetInstance().FrontpageItems));
        }
    }
}
