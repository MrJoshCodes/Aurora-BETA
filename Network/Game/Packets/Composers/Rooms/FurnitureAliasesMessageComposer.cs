namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class FurnitureAliasesMessageComposer : MessageComposer
    {
        public FurnitureAliasesMessageComposer()
            : base(297)
        {
            AppendVL64(0);
        }
    }
}
