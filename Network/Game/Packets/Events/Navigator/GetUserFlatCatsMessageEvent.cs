using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    class GetUserFlatCatsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.SendComposer(new UserFlatCatsComposer(Engine.Locator.NavigatorController.GetUserCategories(client.Player.Rank)));
        }
    }
}
