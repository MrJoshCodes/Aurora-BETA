﻿using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using AuroraEmu.Network.Game.Packets.Composers.Catalogue;
using AuroraEmu.Network.Game.Packets.Composers.Inventory;
using AuroraEmu.Network.Game.Packets.Composers.Users;

namespace AuroraEmu.Network.Game.Packets.Events.Catalogue
{
    class PurchaseFromCatalogEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int pageId = msgEvent.ReadVL64();
            int productId = msgEvent.ReadVL64();
            string data = msgEvent.ReadString();
            int unk = msgEvent.ReadVL64();

            CatalogProduct product = CatalogController.GetInstance().GetProduct(productId);

            if (product != null)
            {
                bool shortOnCoins = product.PriceCoins > client.Player.Coins;
                bool shortOnPixels = product.PricePixels > client.Player.Pixels;

                if (shortOnCoins || shortOnPixels)
                {
                    client.SendComposer(new NotEnoughBalanceMessageComposer(shortOnCoins, shortOnPixels));
                }
                else
                {
                    if (product.PriceCoins > 0)
                    {
                        client.Player.Coins -= product.PriceCoins;
                        client.QueueComposer(new CreditBalanceMessageComposer(client.Player.Coins));
                    }

                    if (product.PricePixels > 0)
                    {
                        client.Player.Pixels -= product.PricePixels;
                        client.QueueComposer(new HabboActivityPointNotificationMessageComposer(client.Player.Pixels, 0));
                    }

                    string extraData = CatalogController.GetInstance().GenerateExtraData(product, data);

                    ItemController.GetInstance().GiveItem(client, product, extraData);

                    client.QueueComposer(new PurchaseOKMessageComposer(product));

                    if (client.Items != null)
                        client.QueueComposer(new FurniListUpdateComposer());

                    client.Flush();
                }
            }
        }
    }
}