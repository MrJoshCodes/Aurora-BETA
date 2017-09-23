using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using AuroraEmu.Game.Items.Dimmer;
using AuroraEmu.Game.Items.Handlers;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AuroraEmu.DI.Game.Items
{
    public interface IItemController
    {
        Dictionary<HandleType, IItemHandler> Handlers { get; set; }

        void ReloadHandlers();

        void ReloadTemplates();

        ItemDefinition GetTemplate(int id);

        void GiveItem(Client client, CatalogProduct product, string extraData);
        
        void GiveItem(Client client, ItemDefinition template, string extraData);

        ConcurrentDictionary<int, Item> GetItemsInRoom(int roomId);

        Dictionary<int, Item> GetItemsFromOwner(int ownerId);

        void AddFloorItem(int itemId, int x, int y, int rot, int roomId);

        void AddWallItem(int itemId, string wallposition, int roomId);

        DimmerData GetDimmerData(int itemId);
    }
}
