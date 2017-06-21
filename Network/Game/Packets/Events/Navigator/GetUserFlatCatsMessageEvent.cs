using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Navigator;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    class GetUserFlatCatsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.SendComposer(new UserFlatCatsComposer(NavigatorController.GetInstance().GetUserCategories(client.Player.Rank)));
        }
    }
}
