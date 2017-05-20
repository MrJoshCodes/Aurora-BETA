using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;

namespace AuroraEmu.Network.Game.Packets.Events.Catalogue
{
    class GetCatalogPageMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int pageId = msgEvent.ReadVL64();
            CatalogPage page = Engine.Game.Catalog.GetPage(pageId);

            if (page != null && !page.Development && page.HasContent)
            {
                MessageComposer composer = new MessageComposer(127);
                composer.AppendVL64(pageId);
                composer.AppendString(page.Layout);

                composer.AppendVL64(page.PageData["image"].Count);

                foreach (CatalogPageData image in page.PageData["image"])
                {
                    composer.AppendString(image.Value);
                }

                composer.AppendVL64(page.PageData["text"].Count);

                foreach (CatalogPageData image in page.PageData["text"])
                {
                    composer.AppendString(image.Value);
                }

                composer.AppendVL64(0);
                client.SendComposer(composer);
            }
        }
    }
}
