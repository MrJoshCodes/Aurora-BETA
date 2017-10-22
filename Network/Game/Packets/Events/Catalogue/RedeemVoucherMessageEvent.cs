using AuroraEmu.Game.Badges.Models;
using AuroraEmu.Game.Catalog.Models.Vouchers;
using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Catalogue;

namespace AuroraEmu.Network.Game.Packets.Events.Catalogue
{
    public class RedeemVoucherMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            var voucherText = msg.ReadString();
            
            if(Engine.Locator.CatalogController.Vouchers.TryGetValue(voucherText, out Voucher voucher))
            {
                switch (voucher.Type)
                {
                    case VoucherType.Credit:
                        if (!int.TryParse(voucher.Reward, out int credits)) break;
                        client.IncreaseCredits(credits);
                        break;
                    case VoucherType.Pixel:
                        if (!int.TryParse(voucher.Reward, out int pixels)) break;
                        client.IncreasePixels(pixels);
                        break;
                    case VoucherType.Item:
                        if (!int.TryParse(voucher.Reward, out int definitionId)) break;
                        var definition = Engine.Locator.ItemController.GetTemplate(definitionId);

                        if (definition == null)
                        {
                            client.SendComposer(new VoucherRedeemErrorMessageComposer()); 
                            return;
                        }
                        
                        Engine.Locator.ItemController.GiveItem(client, definition, string.Empty);
                        break;
                    case VoucherType.Badge:
                        var id = Engine.Locator.BadgeController.Dao.InsertBadge(client.Player.Id, voucher.Reward);
                        client.Player.BadgesComponent.Badges.Add(id, new Badge(id, voucher.Reward));
                        break;
                    default:
                        client.SendComposer(new VoucherRedeemErrorMessageComposer());
                        break;
                }
                client.SendComposer(new VoucherRedeemOkMessageComposer());
            }
            else
            {
                client.SendComposer(new VoucherRedeemErrorMessageComposer());
            }
        }
    }
}
