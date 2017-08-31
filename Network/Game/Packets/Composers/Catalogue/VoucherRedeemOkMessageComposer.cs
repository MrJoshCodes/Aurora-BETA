using AuroraEmu.Game.Items;

namespace AuroraEmu.Network.Game.Packets.Composers.Catalogue
{
    public class VoucherRedeemOkMessageComposer : MessageComposer
    {
        public VoucherRedeemOkMessageComposer()
            : base(212)
        {
            AppendString("HAhaha ");
            AppendString("JAg är fucking kung");
        }

        public VoucherRedeemOkMessageComposer(Item item)
            : base(212)
        {
            AppendString(item.Definition.SwfName);
            AppendString(item.Definition.SwfName);
        }
    }
}
