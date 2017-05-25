using AuroraEmu.Database;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogController
    {
        private readonly IReadOnlyDictionary<int, CatalogPage> pages;
        private readonly IReadOnlyDictionary<int, CatalogProduct> products;
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

                products = session.CreateCriteria<CatalogProduct>().List<CatalogProduct>().ToDictionary(x => x.Id);
            }

             Engine.Logger.Info($"Loaded {pages.Count} catalog pages and {products.Count} products.");
        }

        public CatalogPage GetPage(int id)
        {
            CatalogPage page;

            if (pages.TryGetValue(id, out page))
                return page;

            return null;
        }

        public IReadOnlyList<CatalogPage> GetPagesByParent(int parentId)
        {
            List<CatalogPage> childs = new List<CatalogPage>();

            foreach (CatalogPage page in pages.Values)
            {
                if (page.ParentId == parentId)
                    childs.Add(page);
            }

            return childs;
        }

        public List<CatalogProduct> GetProducts(int page)
        {
            List<CatalogProduct> productsInPage = new List<CatalogProduct>();

            foreach (CatalogProduct product in products.Values)
            {
                if (product.PageId == page)
                    productsInPage.Add(product);
            }

            return productsInPage;
        }

        public static CatalogController GetInstance()
        {
            if (catalogControllerInstance == null)
                catalogControllerInstance = new CatalogController();
            return catalogControllerInstance;
        }
    }
}
