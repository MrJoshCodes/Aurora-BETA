using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Catalogue;
using System.Collections.Generic;

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
                List<CatalogProduct> products = CatalogController.GetInstance().GetProducts(pageId);
                client.SendComposer(new CatalogPageMessageComposer(pageId, page, products));
            }
        }
    }
}
