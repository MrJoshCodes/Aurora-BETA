using AuroraEmu.Database;
using System.Collections.Generic;
using System.Data;

namespace AuroraEmu.Game.Items
{
    public class ItemController
    {
        private static ItemController instance;

        private Dictionary<int, Item> items;

        public ItemController()
        {
            items = new Dictionary<int, Item>();

            ReloadTemplates();
        }

        public void ReloadTemplates()
        {
            items.Clear();

            DataTable result;

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("SELECT * FROM items;");
                dbConnection.Open();

                result = dbConnection.GetTable();
            }

            if (result != null)
            {
                foreach (DataRow row in result.Rows)
                {
                    items.Add((int)row["id"], new Item(row));
                }
            }

            Engine.Logger.Info($"Loaded {items.Count} item templates.");
        }

        public Item GetTemplate(int id)
        {
            if (items.TryGetValue(id, out Item item))
                return item;

            return null;
        }

        public static ItemController GetInstance()
        {
            if (instance == null)
                instance = new ItemController();

            return instance;
        }
    }
}
