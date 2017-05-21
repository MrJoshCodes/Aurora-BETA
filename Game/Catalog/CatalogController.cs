using AuroraEmu.Database;
using AuroraEmu.Network.Game.Packets;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogController
    {
        private readonly IReadOnlyDictionary<int, CatalogPage> pages;
        private static CatalogController catalogControllerInstance;

        public CatalogController()
        {
            using (ISession session = DatabaseHelper.GetInstance().SessionFactory.OpenSession())
            {
                pages = session.CreateCriteria<CatalogPage>().List<CatalogPage>().ToDictionary(x => x.Id);
                
                IReadOnlyList<CatalogPageData> data = session.CreateCriteria<CatalogPageData>().List<CatalogPageData>() as IReadOnlyList<CatalogPageData>;

                foreach(CatalogPageData pageData in data)
                {
                    if (!pages[pageData.PageId].PageData.ContainsKey(pageData.Type))
                    {
                        pages[pageData.PageId].PageData.Add(pageData.Type, new List<CatalogPageData>());
                    }

                    pages[pageData.PageId].PageData[pageData.Type].Add(pageData);
                }
            }

             Engine.Logger.Info($"Loaded {pages.Count} catalog pages.");
        }

        public CatalogPage GetPage(int id)
        {
            CatalogPage page;

            if (pages.TryGetValue(id, out page))
                return page;

            return null;
        }

        private IReadOnlyList<CatalogPage> GetPagesByParent(int parentId)
        {
            List<CatalogPage> childs = new List<CatalogPage>();

            foreach (CatalogPage page in pages.Values)
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

            foreach (CatalogPage page in pages.Values)
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

        public static CatalogController GetInstance()
        {
            if (catalogControllerInstance == null)
                catalogControllerInstance = new CatalogController();
            return catalogControllerInstance;
        }
    }
}
