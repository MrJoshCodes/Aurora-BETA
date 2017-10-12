using System.Collections.Concurrent;
using System.Collections.Generic;
using AuroraEmu.Game.Clients;
using AuroraEmu.DI.Game.Items;
using AuroraEmu.Database;
using AuroraEmu.Game.Items.Handlers;
using AuroraEmu.Game.Catalog.Models;
using AuroraEmu.Game.Items.Models;
using AuroraEmu.Game.Items.Models.Dimmer;
using AuroraEmu.DI.Database.DAO;

namespace AuroraEmu.Game.Items
{
    public class ItemController : IItemController
    {
        private readonly Dictionary<int, ItemDefinition> _items;
        public Dictionary<HandleType, IItemHandler> Handlers { get; set; }
        public Dictionary<int, DimmerData> Dimmers { get; set; }
        public IItemDao Dao { get; }

        public ItemController(IItemDao dao)
        {
            Dao = dao;
            _items = new Dictionary<int, ItemDefinition>();
            Dimmers = new Dictionary<int, DimmerData>();

            ReloadTemplates();
            ReloadHandlers();
        }

        public void ReloadHandlers()
        {
            Handlers = new Dictionary<HandleType, IItemHandler>()
            {
                { HandleType.DICE, new DiceHandler() },
                { HandleType.COLOR_WHEEL, new ColorWheelHandler() }
            };
        }

        public void ReloadTemplates()
        {
            _items.Clear();
            Dao.ReloadTemplates(_items);
            Engine.Logger.Info($"Loaded {_items.Count} item templates.");
        }

        public ItemDefinition GetTemplate(int id) =>
            _items.TryGetValue(id, out ItemDefinition item) ? item : null;

        public void GiveItem(Client client, CatalogProduct product, string extraData) =>
            Dao.GiveItem(client, product, extraData);
        
        public void GiveItem(Client client, ItemDefinition template, string extraData) =>
            Dao.GiveItem(client, template, extraData);

        public ConcurrentDictionary<int, Item> GetItemsInRoom(int roomId) =>
            Dao.GetItemsInRoom(roomId);

        public Dictionary<int, Item> GetItemsFromOwner(int ownerId) =>
            Dao.GetItemsFromOwner(ownerId);

        public void AddFloorItem(int itemId, int x, int y, int rot, int roomId) =>
            Dao.AddFloorItem(itemId, x, y, rot, roomId);

        public void AddWallItem(int itemId, string wallposition, int roomId) =>
            Dao.AddWallItem(itemId, wallposition, roomId);

        public DimmerData GetDimmerData(int itemId)
        {
            if (Dimmers.TryGetValue(itemId, out DimmerData data))
                return data;

            DimmerData dimmerData = null;
            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT * FROM room_dimmer WHERE item_id = @itemId");
                dbConnection.AddParameter("@itemId", itemId);
                using (var reader = dbConnection.ExecuteReader())
                    if (reader.Read())
                    {
                        dimmerData = new DimmerData(reader, false);
                        Dimmers.Add(dimmerData.ItemId, dimmerData);
                        return dimmerData;
                    }
                    else
                    {
                        return NewDimmerData(itemId);
                    }
            }
        }

        public DimmerData NewDimmerData(int itemId)
        {
            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("INSERT INTO room_dimmer(item_id, enabled, current_preset, preset_one, preset_two,preset_three) VALUES(@itemId,DEFAULT,1,'#000000,255,0','#000000,255,0','#000000,255,0');");
                dbConnection.AddParameter("@itemId", itemId);
                dbConnection.Execute();
                return GetDimmerData(itemId);
            }
        }
    }
}
