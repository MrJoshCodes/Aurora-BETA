using System.Collections.Generic;
using AuroraEmu.DI.Game.Catalog;
using System;
using System.Linq;
using AuroraEmu.Game.Catalog.Models.Vouchers;
using AuroraEmu.Game.Catalog.Models;
using AuroraEmu.DI.Database.DAO;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogController : ICatalogController
    {
        private readonly Dictionary<int, List<CatalogDealItem>> _deals;
        private readonly Dictionary<int, CatalogPage> _pages;
        private readonly Dictionary<int, CatalogProduct> _products;
        public Dictionary<string, Voucher> Vouchers { get; }
        public ICatalogDao Dao { get; }

        public CatalogController(ICatalogDao dao)
        {
            Dao = dao;
            _deals = new Dictionary<int, List<CatalogDealItem>>();
            _pages = new Dictionary<int, CatalogPage>();
            _products = new Dictionary<int, CatalogProduct>();
            Vouchers = new Dictionary<string, Voucher>();

            ReloadPages();
            ReloadProducts();
            ReloadDeals();
            ReloadVouchers();
        }

        public void ReloadPages()
        {
            _pages.Clear();
            Dao.ReloadCatalogPage(_pages);

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

        public void ReloadProducts()
        {
            _products.Clear();
            Dao.ReloadProducts(_products);

            Engine.Logger.Info($"Loaded {_products.Count} catalogue products.");
        }

        public void ReloadDeals()
        {
            _deals.Clear();
            Dao.ReloadDeals(_deals);

            Engine.Logger.Info($"Loaded {_deals.Count} deals.");
        }

        public void ReloadVouchers()
        {
            Vouchers.Clear();
            Dao.ReloadVouchers(Vouchers);

            Engine.Logger.Info($"Loaded {Vouchers.Count} vouchers.");
        }

        public CatalogPage GetPage(int id) =>
            _pages.TryGetValue(id, out CatalogPage page) ? page : null;

        public CatalogProduct GetProduct(int id) =>
            _products.TryGetValue(id, out CatalogProduct product) ? product : null;

        public List<CatalogDealItem> GetDeal(int dealId) =>
            _deals.TryGetValue(dealId, out List<CatalogDealItem> dealItems) ? dealItems : null;

        public List<CatalogPage> GetPages(int parent) =>
            _pages.Values.Where(page => page.ParentId == parent).ToList();

        public List<CatalogProduct> GetProducts(int pageId) =>
            _products.Values.Where(product => product.PageId == pageId).ToList();
    }
}