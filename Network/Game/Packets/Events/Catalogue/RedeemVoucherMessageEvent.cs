using AuroraEmu.Game.Catalog.Voucher;
using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Catalogue;

namespace AuroraEmu.Network.Game.Packets.Events.Catalogue
{
    public class RedeemVoucherMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            string voucherText = msg.ReadString();
            
            if(Engine.MainDI.CatalogController.Vouchers.TryGetValue(voucherText, out Voucher voucher))
            {
                switch (voucher.Type)
                {
                    case VoucherType.Credit:
                        client.IncreaseCredits(voucher.Amount);
                        break;
                    case VoucherType.Pixel:
                        client.IncreasePixels(voucher.Amount);
                        break;
                    case VoucherType.Item:
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
