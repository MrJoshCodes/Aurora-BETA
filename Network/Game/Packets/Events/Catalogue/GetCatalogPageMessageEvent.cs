using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Catalogue;

namespace AuroraEmu.Network.Game.Packets.Events.Catalogue
{
    class GetCatalogPageMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int pageId = msgEvent.ReadVL64();
            CatalogPage page = CatalogController.GetInstance().GetPage(pageId);
            if (page != null && !page.Development && page.HasContent)
            {
                client.SendComposer(new CatalogPageMessageComposer(pageId, page));
            }
        }
    }
}
