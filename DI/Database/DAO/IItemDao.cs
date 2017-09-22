using System.Collections.Concurrent;
using System.Collections.Generic;
using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;

namespace AuroraEmu.DI.Database.DAO
{
    public interface IItemDao
    {
        Dictionary<int, ItemDefinition> ReloadTemplates(Dictionary<int, ItemDefinition> items);

        void GiveItem(Client client, CatalogProduct product, string extraData);
        
        void GiveItem(Client client, ItemDefinition template, string extraData);
        
        ConcurrentDictionary<int, Item> GetItemsInRoom(int roomId);

        Dictionary<int, Item> GetItemsFromOwner(int ownerId);

        void UpdateItem(int itemId, int x, int y, int rot, object roomId);
    }
}