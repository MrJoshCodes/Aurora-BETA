using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;

namespace AuroraEmu.DI.Database.DAO
{
    public interface IItemDao
    {
        DataTable ReloadTemplates();

        void GiveItem(Client client, CatalogProduct product, string extraData);
        
        ConcurrentDictionary<int, Item> GetItemsInRoom(int roomId);

        Dictionary<int, Item> GetItemsFromOwner(int ownerId);
    }
}