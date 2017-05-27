using AuroraEmu.Database;
using System.Collections.Generic;
using System.Data;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogController
    {
        private static CatalogController instance;

        private Dictionary<int, CatalogPage> pages;
        private Dictionary<int, CatalogProduct> products;

        public CatalogController()
        {
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
                dbConnection.SetQuery("SELECT * FROM catalogue_pages;");
                result = dbConnection.GetTable();
                dbConnection.SetQuery("SELECT page_id,type,value FROM catalogue_pages_data;");
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

        public void ReloadProducts()
        {
            products.Clear();

            DataTable table;

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("SELECT * FROM catalogue_products;");
                table = dbConnection.GetTable();
            }

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    products.Add((int)row["id"], new CatalogProduct(row));
                }
            }

            Engine.Logger.Info($"Loaded {products.Count} catalogue products.");
        }

        public CatalogPage GetPage(int id)
        {
            CatalogPage page;

            if (pages.TryGetValue(id, out page))
                return page;

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

        public static CatalogController GetInstance()
        {
            if (instance == null)
                instance = new CatalogController();

            return instance;
        }
    }
}
