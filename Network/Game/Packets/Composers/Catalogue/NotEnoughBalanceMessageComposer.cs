namespace AuroraEmu.Network.Game.Packets.Composers.Catalogue
{
    class NotEnoughBalanceMessageComposer : MessageComposer
    {
        public NotEnoughBalanceMessageComposer(bool notEnoughCredits, bool notEnoughPixels)
            : base(68)
        {
            AppendVL64(notEnoughCredits);
            AppendVL64(notEnoughPixels);
        }
    }
}
