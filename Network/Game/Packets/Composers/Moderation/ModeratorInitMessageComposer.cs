namespace AuroraEmu.Network.Game.Packets.Composers.Moderation
{
    public class ModeratorInitMessageComposer : MessageComposer
    {
        public ModeratorInitMessageComposer()
            : base(531)
        {
            AppendVL64(-1);
            AppendVL64(0); // Tickets probably?
            AppendVL64(0); // Some presets
        }
    }
}