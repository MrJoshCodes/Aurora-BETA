using System.Collections.Concurrent;
using System.Collections.Generic;
using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using AuroraEmu.DI.Game.Items;
using AuroraEmu.Database;
using AuroraEmu.Game.Items.Handlers;

namespace AuroraEmu.Game.Items
{
    public class ItemController : IItemController
    {
        private readonly Dictionary<int, ItemDefinition> _items;
        public Dictionary<HandleType, IItemHandler> Handlers { get; set; }

        public ItemController()
        {
            _items = new Dictionary<int, ItemDefinition>();

            ReloadTemplates();
            ReloadHandlers();
        }

        public void ReloadHandlers()
        {
            Handlers = new Dictionary<HandleType, IItemHandler>()
            {
                { HandleType.DICE, new DiceHandler() }
            };
        }

        public void ReloadTemplates()
        {
            Engine.MainDI.ItemDao.ReloadTemplates(_items);
            Engine.Logger.Info($"Loaded {_items.Count} item templates.");
        }

        public ItemDefinition GetTemplate(int id)
        {
            return _items.TryGetValue(id, out ItemDefinition item) ? item : null;
        }

        public void GiveItem(Client client, CatalogProduct product, string extraData)
        {
            Engine.MainDI.ItemDao.GiveItem(client, product, extraData);
        }
        
        public void GiveItem(Client client, ItemDefinition template, string extraData)
        {
            Engine.MainDI.ItemDao.GiveItem(client, template, extraData);
        }

        public ConcurrentDictionary<int, Item> GetItemsInRoom(int roomId)
        {
            return Engine.MainDI.ItemDao.GetItemsInRoom(roomId);
        }

        public Dictionary<int, Item> GetItemsFromOwner(int ownerId)
        {
            return Engine.MainDI.ItemDao.GetItemsFromOwner(ownerId);
        }

        public void AddFloorItem(int itemId, int x, int y, int rot, int roomId)
        {
            using(DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
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

        public void AddWallItem(int itemId, string wallposition, int roomId)
        {
            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("UPDATE items SET room_id = @roomId, wallposition = @wallposition WHERE id = @itemId LIMIT 1");
                dbConnection.AddParameter("@roomId", roomId);
                dbConnection.AddParameter("@wallposition", wallposition);
                dbConnection.AddParameter("@itemId", itemId);
                dbConnection.Execute();
            }
        }
    }
}
