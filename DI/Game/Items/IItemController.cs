using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Catalog.Models;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using AuroraEmu.Game.Items.Handlers;
using AuroraEmu.Game.Items.Models;
using AuroraEmu.Game.Items.Models.Dimmer;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AuroraEmu.Game.Players.Models;

namespace AuroraEmu.DI.Game.Items
{
    public interface IItemController
    {
        IItemDao Dao { get; }

        Dictionary<HandleType, IItemHandler> Handlers { get; set; }

        void ReloadHandlers();

        void ReloadTemplates();

        ItemDefinition GetTemplate(int id);

        void GiveItem(Client client, CatalogProduct product, string extraData);
        
        void GiveItem(Client client, ItemDefinition template, string extraData);
        
        void GiveItem(Player targetUser, CatalogProduct product, string extraData);
        
        int GiveItem(Player targetUser, ItemDefinition template, string extraData);

        ConcurrentDictionary<int, Item> GetItemsInRoom(int roomId);

        Dictionary<int, Item> GetItemsFromOwner(int ownerId);

        void AddFloorItem(int itemId, int x, int y, int rot, int roomId);

        void AddWallItem(int itemId, string wallposition, int roomId);

        DimmerData GetDimmerData(int itemId);

        ItemDefinition GetRandomPresent();

        void CreatePresent(int definitionId, int playerId, int giftId, string data);
        
        (int, string) GetPresent(int presentId, int playerId);

        void DeletePresent(int presentId);
    }
}
