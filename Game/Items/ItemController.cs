using AuroraEmu.Database;
using System.Collections.Generic;
using System.Data;

namespace AuroraEmu.Game.Items
{
    public class ItemController
    {
        private static ItemController instance;

        private Dictionary<int, ItemDefinition> items;

        public ItemController()
        {
            items = new Dictionary<int, ItemDefinition>();

            ReloadTemplates();
        }

        public void ReloadTemplates()
        {
            items.Clear();

            DataTable result;

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("SELECT * FROM item_definitions;");
                dbConnection.Open();

                result = dbConnection.GetTable();
            }

            if (result != null)
            {
                foreach (DataRow row in result.Rows)
                {
                    items.Add((int)row["id"], new ItemDefinition(row));
                }
            }

            Engine.Logger.Info($"Loaded {items.Count} item templates.");
        }

        public ItemDefinition GetTemplate(int id)
        {
            if (items.TryGetValue(id, out ItemDefinition item))
                return item;

            return null;
        }

        public Dictionary<int, Item> GetItemsInRoom(int roomId)
        {
            Dictionary<int, Item> items = new Dictionary<int, Item>();

            DataTable result;

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("SELECT * FROM items WHERE room_id = @roomId");
                dbConnection.AddParameter("@roomId", roomId);

                result = dbConnection.GetTable();
            }

            foreach (DataRow row in result.Rows)
            {
                items.Add((int)row["id"], new Item(row));
            }

            return items;
        }

        public static ItemController GetInstance()
        {
            if (instance == null)
                instance = new ItemController();

            return instance;
        }
    }
}
