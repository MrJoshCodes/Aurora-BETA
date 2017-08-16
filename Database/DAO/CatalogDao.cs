using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Catalog;
using System.Collections.Generic;
using System.Data;

namespace AuroraEmu.Database.DAO
{
    public class CatalogDao : ICatalogDao
    {
        public Dictionary<int, CatalogPage> ReloadCatalogPage(Dictionary<int, CatalogPage> pages)
        {
            pages.Clear();

            DataTable result;
            DataTable result2;

            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM catalog_pages;");
                result = dbConnection.GetTable();
                dbConnection.SetQuery("SELECT page_id,type,value FROM catalog_pages_data;");
                result2 = dbConnection.GetTable();

                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
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
            return pages;
        }

        public DataTable ReloadProducts()
        {
            DataTable table;
            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM catalog_products;");
                table = dbConnection.GetTable();

                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }

            return table;
        }

        public DataTable ReloadDeals()
        {
            DataTable table;
            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM catalog_deals;");
                table = dbConnection.GetTable();

                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }

            return table;
        }
    }
}
