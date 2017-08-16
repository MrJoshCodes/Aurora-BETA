using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AuroraEmu.DI.Game.Items
{
    public interface IItemController
    {
        void ReloadTemplates();

        ItemDefinition GetTemplate(int id);

        void GiveItem(Client client, CatalogProduct product, string extraData);

        ConcurrentDictionary<int, Item> GetItemsInRoom(int roomId);

        Dictionary<int, Item> GetItemsFromOwner(int ownerId);
    }
}
