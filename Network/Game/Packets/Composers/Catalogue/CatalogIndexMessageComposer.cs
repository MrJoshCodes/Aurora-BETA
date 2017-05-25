using AuroraEmu.Game.Catalog;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Catalogue
{
    public class CatalogIndexMessageComposer : MessageComposer
    {
        public CatalogIndexMessageComposer() : base(126)
        {
            IReadOnlyList<CatalogPage> categories = CatalogController.GetInstance().GetPagesByParent(0);

            AppendVL64(false);
            AppendVL64(0);
            AppendVL64(0);
            AppendVL64(-1);
            AppendString("");
            AppendVL64(false);
            AppendVL64(categories.Count);

            foreach(CatalogPage category in categories)
            {
                SerializePage(category);
            }
        }

        private void SerializePage(CatalogPage page)
        {
            IReadOnlyList<CatalogPage> children = CatalogController.GetInstance().GetPagesByParent(page.Id);

            AppendVL64(page.Visible);
            AppendVL64(page.IconColor);
            AppendVL64(page.IconImage);
            AppendVL64(page.Id);
            AppendString(page.Name);
            AppendVL64(page.Development);
            AppendVL64(children.Count);

            foreach (CatalogPage child in children)
            {
                SerializePage(child);
            }
        }
    }
}
