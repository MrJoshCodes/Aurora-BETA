using AuroraEmu.Game.Catalog.Models;
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
            CatalogPage page = Engine.MainDI.CatalogController.GetPage(pageId);
            if (page != null && !page.Development && page.HasContent)
            {
                List<CatalogProduct> products = Engine.MainDI.CatalogController.GetProducts(pageId);
                client.SendComposer(new CatalogPageMessageComposer(pageId, page, products));
            }
        }
    }
}