using System.Collections.Generic;
using System.Data;
using AuroraEmu.DI.Game.Catalog;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogController : ICatalogController
    {
        private readonly Dictionary<int, List<CatalogDealItem>> _deals;
        private readonly Dictionary<int, CatalogPage> _pages;
        private readonly Dictionary<int, CatalogProduct> _products;

        public CatalogController()
        {
            _deals = new Dictionary<int, List<CatalogDealItem>>();
            _pages = new Dictionary<int, CatalogPage>();
            _products = new Dictionary<int, CatalogProduct>();

            ReloadPages();
            ReloadProducts();
            ReloadDeals();
        }

        public void ReloadPages()
        {
            Engine.MainDI.CatalogDao.ReloadCatalogPage(_pages);

            Engine.Logger.Info($"Loaded {_pages.Count} catalogue pages.");
        }

        public string GenerateExtraData(CatalogProduct product, string extraData)
        {
            switch (product.Template.SpriteType)
            {
                case "poster":
                    return product.Data;
                default:
                    return "";
            }
        }

        public List<CatalogDealItem> GetDeal(int dealId)
        {
            if (_deals.TryGetValue(dealId, out List<CatalogDealItem> dealItems))
                return dealItems;

            return null;
        }

        public void ReloadProducts()
        {
            _products.Clear();
            DataTable table = Engine.MainDI.CatalogDao.ReloadProducts();

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    _products.Add((int) row["id"], new CatalogProduct(row));
                }
            }

            Engine.Logger.Info($"Loaded {_products.Count} catalogue products.");
        }

        public void ReloadDeals()
        {
            DataTable table = Engine.MainDI.CatalogDao.ReloadDeals();

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int dealId = (int) row["id"];

                    if (!_deals.TryGetValue(dealId, out List<CatalogDealItem> item))
                        _deals.Add(dealId, new List<CatalogDealItem>());

                    _deals[dealId].Add(new CatalogDealItem(row));
                }
            }

            Engine.Logger.Info($"Loaded {_deals.Count} deals");
        }

        public CatalogPage GetPage(int id)
        {
            if (_pages.TryGetValue(id, out CatalogPage page))
                return page;

            return null;
        }

        public CatalogProduct GetProduct(int id)
        {
            if (_products.TryGetValue(id, out CatalogProduct product))
                return product;

            return null;
        }

        public List<CatalogPage> GetPages(int parent)
        {
            List<CatalogPage> pagesInParent = new List<CatalogPage>();

            foreach (CatalogPage page in _pages.Values)
            {
                if (page.ParentId == parent)
                    pagesInParent.Add(page);
            }

            return pagesInParent;
        }

        public List<CatalogProduct> GetProducts(int pageId)
        {
            List<CatalogProduct> productsInPage = new List<CatalogProduct>();

            foreach (CatalogProduct product in _products.Values)
            {
                if (product.PageId == pageId)
                    productsInPage.Add(product);
            }

            return productsInPage;
        }
    }
}