using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items.Models;
using AuroraEmu.Game.Players.Models;
using AuroraEmu.Network.Game.Packets.Composers.Catalogue;
using AuroraEmu.Network.Game.Packets.Composers.Inventory;
using AuroraEmu.Network.Game.Packets.Composers.Users;

namespace AuroraEmu.Network.Game.Packets.Events.Catalogue
{
    class PurchaseFromCatalogEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            var pageId = msgEvent.ReadVL64();
            var productId = msgEvent.ReadVL64();
            var data = msgEvent.ReadString();
            var isGift = msgEvent.ReadVL64();
            
            var product = Engine.Locator.CatalogController.GetProduct(productId);
            
            if (product == null) 
                return;
            
            var shortOnCoins = product.PriceCoins > client.Player.Coins;
            var shortOnPixels = product.PricePixels > client.Player.Pixels;

            if (shortOnCoins || shortOnPixels)
            {
                client.SendComposer(new NotEnoughBalanceMessageComposer(shortOnCoins, shortOnPixels));
                return;
            }

            if (isGift == 1)
            {
                var targetUsername = msgEvent.ReadString();
                var message = msgEvent.ReadString();
                
                // First, check if username exists
                Player player = Engine.Locator.PlayerController.GetPlayerByName(targetUsername);

                if (player == null)
                {
                    // TODO: Send error 
                    return;
                }
                
                if (product.PriceCoins > 0)
                {
                    client.DecreaseCredits(product.PriceCoins);
                }

                if (product.PricePixels > 0)
                {
                    client.DecreasePixels(product.PricePixels);
                }

                ItemDefinition definition = Engine.Locator.ItemController.GetRandomPresent();

                int giftId = Engine.Locator.ItemController.GiveItem(player, definition, "!" + message);
                
                Engine.Locator.ItemController.CreatePresent(product.TemplateId, player.Id, giftId, data);
                
                client.QueueComposer(new PurchaseOKMessageComposer(product));

                Client targetClient = Engine.Locator.ClientController.GetClientByHabbo(player.Id);
                
                if (targetClient != null && targetClient.Items != null)
                    targetClient.SendComposer(new FurniListUpdateComposer());

                client.Flush();
            }
            else
            {
                if (product.PriceCoins > 0)
                {
                    client.DecreaseCredits(product.PriceCoins);
                }

                if (product.PricePixels > 0)
                {
                    client.DecreasePixels(product.PricePixels);
                }
                
                if (product.IsDeal)
                {
                    var dealItems = Engine.Locator.CatalogController.GetDeal(product.DealId);

                    foreach (var dealItem in dealItems)
                    {
                        for (var i = 0; i < dealItem.Amount; i++)
                        {
                            Engine.Locator.ItemController.GiveItem(client, dealItem.Template, string.Empty);
                        }
                    }
                }
                else
                {
                    var extraData = Engine.Locator.CatalogController.GenerateExtraData(product, data).Replace("{USERNAME}", client.Player.Username);
                    
                    for (var i = 0; i < product.Amount; i++)
                    {
                        Engine.Locator.ItemController.GiveItem(client, product, extraData);
                    }
                }

                client.QueueComposer(new PurchaseOKMessageComposer(product));

                if (client.Items != null)
                    client.QueueComposer(new FurniListUpdateComposer());

                client.Flush();
            }
        }
    }
}
