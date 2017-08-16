using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    class GetPopularRoomTagsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.SendComposer(new PopularRoomTagsResultComposer(msgEvent.ReadVL64(), Engine.MainDI.NavigatorController.Categories.Values));
        }
    }
}
