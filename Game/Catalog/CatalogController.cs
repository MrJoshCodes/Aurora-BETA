using AuroraEmu.Database;
using System.Collections.Generic;
using System.Data;
using System;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogController
    {
        private static CatalogController instance;

        private Dictionary<int, List<CatalogDealItem>> deals;
        private Dictionary<int, CatalogPage> pages;
        private Dictionary<int, CatalogProduct> products;

        public CatalogController()
        {
            deals = new Dictionary<int, List<Catalog.CatalogDealItem>>();
            pages = new Dictionary<int, CatalogPage>();
            products = new Dictionary<int, CatalogProduct>();

            ReloadPages();
            ReloadProducts();
        }

        public void ReloadPages()
        {
            pages.Clear();

            DataTable result;
            DataTable result2;

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("SELECT * FROM catalog_pages;");
                result = dbConnection.GetTable();
                dbConnection.SetQuery("SELECT page_id,type,value FROM catalog_pages_data;");
                result2 = dbConnection.GetTable();
            }

            if (result != null)
            {
                foreach (DataRow row in result.Rows)
                {
                    pages.Add((int)row["id"], new CatalogPage(row));
                }
            }

            if (result2 != null)
            {
                foreach (DataRow row in result2.Rows)
                {
                    pages[(int)row["page_id"]].Data[(string)row["type"]].Add((string)row["value"]);
                }
            }

            Engine.Logger.Info($"Loaded {pages.Count} catalogue pages.");
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
            if (deals.TryGetValue(dealId, out List<CatalogDealItem> dealItems))
                return dealItems;

            return null;
        }

        public void ReloadProducts()
        {
            deals = new Dictionary<int, List<CatalogDealItem>>();
            products.Clear();

            DataTable table;
            DataTable table2;

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("SELECT * FROM catalog_products;");
                table = dbConnection.GetTable();
                dbConnection.SetQuery("SELECT * FROM catalog_deals;");
                table2 = dbConnection.GetTable();
            }

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    products.Add((int)row["id"], new CatalogProduct(row));
                }
            }

            if (table2 != null)
            {
                foreach(DataRow row in table2.Rows)
                {
                    int dealId = (int)row["id"];

                    if (!deals.TryGetValue(dealId, out List<CatalogDealItem> items))
                        deals.Add(dealId, new List<CatalogDealItem>());

                    deals[dealId].Add(new CatalogDealItem(row));
                }
            }

            Engine.Logger.Info($"Loaded {products.Count} catalogue products and {deals.Count} deals.");
        }

        public CatalogPage GetPage(int id)
        {
            if (pages.TryGetValue(id, out CatalogPage page))
                return page;

            return null;
        }

        public CatalogProduct GetProduct(int id)
        {
            if (products.TryGetValue(id, out CatalogProduct product))
                return product;

            return null;
        }

        public List<CatalogPage> GetPages(int parent)
        {
            List<CatalogPage> pagesInParent = new List<CatalogPage>();

            foreach (CatalogPage page in pages.Values)
            {
                if (page.ParentId == parent)
                    pagesInParent.Add(page);
            }

            return pagesInParent;
        }

        public List<CatalogProduct> GetProducts(int pageId)
        {
            List<CatalogProduct> productsInPage = new List<CatalogProduct>();

            foreach (CatalogProduct product in products.Values)
            {
                if (product.PageId == pageId)
                    productsInPage.Add(product);
            }

            return productsInPage;
        }

        public static CatalogController GetInstance()
        {
            if (instance == null)
                instance = new CatalogController();

            return instance;
        }
    }
}
