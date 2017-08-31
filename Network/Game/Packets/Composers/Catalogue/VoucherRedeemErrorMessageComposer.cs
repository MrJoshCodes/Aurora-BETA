namespace AuroraEmu.Network.Game.Packets.Composers.Catalogue
{
    public class VoucherRedeemErrorMessageComposer : MessageComposer
    {
        public VoucherRedeemErrorMessageComposer()
            : base(213)
        {
            AppendVL64(1);
        }
    }
}
