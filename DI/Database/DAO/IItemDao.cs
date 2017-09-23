using System.Collections.Concurrent;
using System.Collections.Generic;
using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using AuroraEmu.Game.Items.Dimmer;

namespace AuroraEmu.DI.Database.DAO
{
    public interface IItemDao
    {
        void ReloadTemplates(Dictionary<int, ItemDefinition> items);

        void GiveItem(Client client, CatalogProduct product, string extraData);
        
        ConcurrentDictionary<int, Item> GetItemsInRoom(int roomId);

        Dictionary<int, Item> GetItemsFromOwner(int ownerId);

        void UpdateItem(int itemId, int x, int y, int rot, object roomId);

        void AddFloorItem(int itemId, int x, int y, int rot, int roomId);

        void AddWallItem(int itemId, string wallposition, int roomId);

        void UpdateItemData(int itemId, string data);

        void UpdateDimmerPreset(DimmerData data);
    }
}