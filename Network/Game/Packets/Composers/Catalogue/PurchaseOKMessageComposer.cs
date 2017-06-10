using AuroraEmu.Game.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraEmu.Network.Game.Packets.Composers.Catalogue
{
    class PurchaseOKMessageComposer : MessageComposer
    {
        public PurchaseOKMessageComposer(CatalogProduct product)
            : base(67)
        {
            SerializeProduct(product);
        }

        private void SerializeProduct(CatalogProduct product)
        {
            AppendVL64(product.Id);
            AppendString(product.Name);
            AppendVL64(product.PriceCoins);
            AppendVL64(product.PricePixels);
            AppendVL64(product.IsDeal ? product.DealItems.Count : 1);

            if (product.IsDeal)
            {
                foreach (CatalogDealItem item in product.DealItems)
                {
                    AppendString(item.Template.SpriteType.ToString());
                    AppendVL64(item.Template.SpriteId);
                    AppendString("");
                    AppendVL64(item.Amount);
                    AppendVL64(-1);
                }
            }
            else
            {
                AppendString(product.Template.SpriteType.ToString());
                AppendVL64(product.Template.SpriteId);
                AppendString(product.Data);
                AppendVL64(product.Amount);
                AppendVL64(-1);
            }
        }
    }
}
