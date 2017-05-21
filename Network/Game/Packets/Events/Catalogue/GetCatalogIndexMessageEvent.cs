using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Catalogue;

namespace AuroraEmu.Network.Game.Packets.Events.Catalogue
{
    class GetCatalogIndexMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            MessageComposer composer = new CatalogIndexMessageComposer();

            CatalogController.GetInstance().SerializeIndex(composer);
            client.SendComposer(composer);
        }
    }
}
