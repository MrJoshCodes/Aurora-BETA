using System.Collections.Generic;
using AuroraEmu.DI.Game.Catalog;
using System;
using System.Linq;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogController : ICatalogController
    {
        private readonly Dictionary<int, List<CatalogDealItem>> _deals;
        private readonly Dictionary<int, CatalogPage> _pages;
        private readonly Dictionary<int, CatalogProduct> _products;
        public Dictionary<string, Voucher.Voucher> Vouchers { get; }

        public CatalogController()
        {
            _deals = new Dictionary<int, List<CatalogDealItem>>();
            _pages = new Dictionary<int, CatalogPage>();
            _products = new Dictionary<int, CatalogProduct>();
            Vouchers = new Dictionary<string, Voucher.Voucher>();

            ReloadPages();
            ReloadProducts();
            ReloadDeals();
            ReloadVouchers();
        }

        public void ReloadPages()
        {
            Engine.MainDI.CatalogDao.ReloadCatalogPage(_pages);

            Engine.Logger.Info($"Loaded {_pages.Count} catalogue pages.");
        }

        public string GenerateExtraData(CatalogProduct product, string extraData)
        {
            switch (product.Template.ItemType)
            {
                case "poster":
                    return product.Data;
                case "trophy":
                    return $"{{USERNAME}} {(char)9} {DateTime.Now.ToString("dd-MM-yyyy")} {(char)9} {extraData}";
                case "dimmer":
                    return "1,1,1,#000000,255";
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
            Engine.MainDI.CatalogDao.ReloadProducts(_products);

            Engine.Logger.Info($"Loaded {_products.Count} catalogue products.");
        }

        public void ReloadDeals()
        {
            Engine.MainDI.CatalogDao.ReloadDeals(_deals);

            Engine.Logger.Info($"Loaded {_deals.Count} deals.");
        }

        public void ReloadVouchers()
        {
            Engine.MainDI.CatalogDao.ReloadVouchers(Vouchers);

            Engine.Logger.Info($"Loaded {Vouchers.Count} vouchers.");
        }

        public CatalogPage GetPage(int id)
        {
            if (_pages.TryGetValue(id, out CatalogPage page))
                return page;

            return null;
        }

        public CatalogProduct GetProduct(int id)
        {
            return _products.TryGetValue(id, out CatalogProduct product) ? product : null;
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

            return productsInPage.OrderBy(item => item.Order).ToList();
        }
    }
}