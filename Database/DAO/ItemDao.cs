using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;

namespace AuroraEmu.Database.DAO
{
    public class ItemDao : IItemDao
    {
        public DataTable ReloadTemplates()
        {
            DataTable result;

            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM item_definitions;");
                result = dbConnection.GetTable();
                
                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }
            return result;
        }
        
        public void GiveItem(Client client, CatalogProduct product, string extraData)
        {
            int id = -1;

            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("INSERT INTO items (owner_id, definition_id, data) VALUES (@ownerId, @definitionId, @data)");
                dbConnection.AddParameter("@ownerId", client.Player.Id);
                dbConnection.AddParameter("@definitionId", product.TemplateId);
                dbConnection.AddParameter("@data", extraData);
                id = dbConnection.Insert();
                
                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
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

            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM items WHERE room_id = @roomId");
                dbConnection.AddParameter("@roomId", roomId);
                result = dbConnection.GetTable();
                
                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
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

            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM items WHERE owner_id = @ownerId AND room_id IS NULL");
                dbConnection.AddParameter("@ownerId", ownerId);

                result = dbConnection.GetTable();
                
                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }

            foreach (DataRow row in result.Rows)
            {
                items.Add((int)row["id"], new Item(row));
            }

            return items;
        }
    }
}