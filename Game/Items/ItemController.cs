﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using AuroraEmu.DI.Game.Items;

namespace AuroraEmu.Game.Items
{
    public class ItemController : IItemController
    {
        private readonly Dictionary<int, ItemDefinition> _items;

        public ItemController()
        {
            _items = new Dictionary<int, ItemDefinition>();

            ReloadTemplates();
        }

        public void ReloadTemplates()
        {
            DataTable result = Engine.MainDI.ItemDao.ReloadTemplates();
            
            if (result != null)
            {
                foreach (DataRow row in result.Rows)
                {
                    _items.Add((int)row["id"], new ItemDefinition(row));
                }
            }

            Engine.Logger.Info($"Loaded {_items.Count} item templates.");
        }

        public ItemDefinition GetTemplate(int id)
        {
            if (_items.TryGetValue(id, out ItemDefinition item))
                return item;

            return null;
        }

        public void GiveItem(Client client, CatalogProduct product, string extraData)
        {
            Engine.MainDI.ItemDao.GiveItem(client, product, extraData);
        }

        public ConcurrentDictionary<int, Item> GetItemsInRoom(int roomId)
        {
            return Engine.MainDI.ItemDao.GetItemsInRoom(roomId);
        }

        public Dictionary<int, Item> GetItemsFromOwner(int ownerId)
        {
            return Engine.MainDI.ItemDao.GetItemsFromOwner(ownerId);
        }
    }
}
