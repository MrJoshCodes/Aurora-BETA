namespace AuroraEmu.Network.Game.Packets.Composers.Users.Clothing
{
    public class WardrobeMessageComposer : MessageComposer
    {
        public WardrobeMessageComposer(int header)
            : base(267)
        {
            AppendVL64(true);
            AppendVL64(0);

        }
    }
}
