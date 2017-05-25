using AuroraEmu.Game.Catalog;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Catalogue
{
    public class CatalogPageMessageComposer : MessageComposer
    {
        public CatalogPageMessageComposer(int pageId, CatalogPage page, List<CatalogProduct> products) : base(127)
        {
            AppendVL64(pageId);
            AppendString(page.Layout);

            AppendVL64(page.PageData["image"].Count);

            foreach (CatalogPageData image in page.PageData["image"])
            {
                AppendString(image.Value);
            }

                AppendVL64(page.PageData["text"].Count);

            foreach (CatalogPageData image in page.PageData["text"])
            {
                AppendString(image.Value);
            }

            AppendVL64(products.Count);

            foreach (CatalogProduct product in products)
            {
                SerializeProduct(product);
            }
        }

        private void SerializeProduct(CatalogProduct product)
        {
            AppendVL64(product.Id);
            AppendString(product.Name);
            AppendVL64(product.PriceCoins);
            AppendVL64(product.PricePixels);
            AppendVL64(1); // deals soon
            AppendString(product.Template.SpriteType.ToString());
            AppendVL64(product.Template.SpriteId);
            AppendString("");
            AppendVL64(product.Amount);
            AppendVL64(-1);
        }
    }
}
