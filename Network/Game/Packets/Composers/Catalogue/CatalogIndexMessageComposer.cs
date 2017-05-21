namespace AuroraEmu.Network.Game.Packets.Composers.Catalogue
{
    public class CatalogIndexMessageComposer : MessageComposer
    {
        public CatalogIndexMessageComposer() : base(126)
        {
            AppendVL64(false);
            AppendVL64(0);
            AppendVL64(0);
            AppendVL64(-1);
            AppendString("");
            AppendVL64(false);
        }
    }
}
