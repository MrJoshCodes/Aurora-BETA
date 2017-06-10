using AuroraEmu.Database;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using System;

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

        public void GiveItem(Client client, CatalogProduct product, string extraData)
        {
            int id = -1;

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("INSERT INTO items (owner_id, definition_id, data) VALUES (@ownerId, @definitionId, @data)");
                dbConnection.AddParameter("@ownerId", client.Player.Id);
                dbConnection.AddParameter("@definitionId", product.TemplateId);
                dbConnection.AddParameter("@data", extraData);

                dbConnection.Open();

                id = dbConnection.Insert();
            }

            if (id > 0 && client.Items != null)
            {
                client.Items.Add(id, new Item(id, client.Player.Id, product.TemplateId, extraData));
            }
        }

        public ConcurrentDictionary<int, Item> GetItemsInRoom(int roomId)
        {
            ConcurrentDictionary<int, Item> items = new ConcurrentDictionary<int, Item>();

            DataTable result;

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("SELECT * FROM items WHERE room_id = @roomId");
                dbConnection.AddParameter("@roomId", roomId);

                dbConnection.Open();

                result = dbConnection.GetTable();
            }

            foreach (DataRow row in result.Rows)
            {
                items.TryAdd((int)row["id"], new Item(row));
            }

            return items;
        }

        public Dictionary<int, Item> GetItemsFromOwner(int ownerId)
        {
            Dictionary<int, Item> items = new Dictionary<int, Item>();

            DataTable result;

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("SELECT * FROM items WHERE owner_id = @ownerId AND room_id IS NULL");
                dbConnection.AddParameter("@ownerId", ownerId);

                dbConnection.Open();

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
