using AuroraEmu.Network.Game.Packets;
using NHibernate;
using System.Collections.Generic;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogController
    {
        private readonly IReadOnlyList<CatalogPage> pages;

        public CatalogController()
        {
            using (ISession session = Engine.Database.SessionFactory.OpenSession())
            {
                pages = session.CreateCriteria<CatalogPage>().List<CatalogPage>() as IReadOnlyList<CatalogPage>;
            }

            Engine.Logger.Info($"Loaded {pages.Count} catalog pages.");
        }

        private IReadOnlyList<CatalogPage> GetPagesByParent(int parentId)
        {
            List<CatalogPage> childs = new List<CatalogPage>();

            foreach (CatalogPage page in pages)
            {
                if (page.ParentId == parentId)
                    childs.Add(page);
            }

            return childs;
        }

        public void SerializeIndex(MessageComposer composer)
        {
            IReadOnlyList<CatalogPage> categories = GetPagesByParent(0);
            composer.AppendVL64(categories.Count);

            foreach (CatalogPage page in pages)
            {
                SerializePage(composer, page);
            }
        }

        private void SerializePage(MessageComposer composer, CatalogPage page)
        {
            IReadOnlyList<CatalogPage> children = GetPagesByParent(page.Id);

            composer.AppendVL64(page.Visible);
            composer.AppendVL64(page.IconColor);
            composer.AppendVL64(page.IconImage);
            composer.AppendVL64(page.Id);
            composer.AppendString(page.Name);
            composer.AppendVL64(page.Development);
            composer.AppendVL64(children.Count);

            foreach (CatalogPage child in children)
            {
                SerializePage(composer, child);
            }
        }
    }
}
