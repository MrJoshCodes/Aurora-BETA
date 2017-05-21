using AuroraEmu.Game.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraEmu.Network.Game.Packets.Composers.Catalogue
{
    public class CatalogPageMessageComposer : MessageComposer
    {
        public CatalogPageMessageComposer(int pageId, CatalogPage page) : base(127)
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

            AppendVL64(0);
        }
    }
}
