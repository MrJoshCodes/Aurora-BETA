namespace AuroraEmu.Network.Game.Packets.Composers.Users
{
    public class HabboActivityPointNotificationMessageComposer : MessageComposer
    {
        public HabboActivityPointNotificationMessageComposer(int pixels, int notification) : base(438)
        {
            AppendVL64(pixels);
            AppendVL64(notification);
        }
    }
}
