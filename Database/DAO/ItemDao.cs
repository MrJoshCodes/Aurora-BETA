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
        public Dictionary<int, ItemDefinition> ReloadTemplates(Dictionary<int, ItemDefinition> items)
        {
            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT * FROM item_definitions;");
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                        items.Add(reader.GetInt32("id"), new ItemDefinition(reader));
            }
            return items;
        }
        
        public void GiveItem(Client client, CatalogProduct product, string extraData)
        {
            int id = -1;

            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("INSERT INTO items (owner_id, definition_id, data) VALUES (@ownerId, @definitionId, @data)");
                dbConnection.AddParameter("@ownerId", client.Player.Id);
                dbConnection.AddParameter("@definitionId", product.TemplateId);
                dbConnection.AddParameter("@data", extraData);
                id = dbConnection.Insert();
            }

            if (id > 0 && client.Items != null)
            {
                client.Items.Add(id, new Item(id, client.Player.Id, product.TemplateId, extraData));
            }
        }
        
        public void GiveItem(Client client, ItemDefinition template, string extraData)
        {
            int id = -1;

            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("INSERT INTO items (owner_id, definition_id, data) VALUES (@ownerId, @definitionId, @data)");
                dbConnection.AddParameter("@ownerId", client.Player.Id);
                dbConnection.AddParameter("@definitionId", template.Id);
                dbConnection.AddParameter("@data", extraData);
                id = dbConnection.Insert();
            }

            if (id > 0 && client.Items != null)
            {
                client.Items.Add(id, new Item(id, client.Player.Id, template.Id, extraData));
            }
        }
        
        public ConcurrentDictionary<int, Item> GetItemsInRoom(int roomId)
        {
            ConcurrentDictionary<int, Item> items = new ConcurrentDictionary<int, Item>();
            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT * FROM items WHERE room_id = @roomId");
                dbConnection.AddParameter("@roomId", roomId);
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                        items.TryAdd(reader.GetInt32("id"), new Item(reader));
            }
            return items;
        }
        
        public Dictionary<int, Item> GetItemsFromOwner(int ownerId)
        {
            Dictionary<int, Item> items = new Dictionary<int, Item>();

            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT * FROM items WHERE owner_id = @ownerId AND room_id IS NULL");
                dbConnection.AddParameter("@ownerId", ownerId);
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                        items.Add(reader.GetInt32("id"), new Item(reader));
            }

            return items;
        }

        public void UpdateItem(int itemId, int x, int y, int rot, object roomId)
        {
            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("UPDATE items SET room_id = @roomId, x = @x, y = @y, rotation = @rot WHERE id = @itemId LIMIT 1");
                dbConnection.AddParameter("@roomId", roomId);
                dbConnection.AddParameter("@x", x);
                dbConnection.AddParameter("@y", y);
                dbConnection.AddParameter("@rot", rot);
                dbConnection.AddParameter("@itemId", itemId);
                dbConnection.Execute();
            }
        }
    }
}