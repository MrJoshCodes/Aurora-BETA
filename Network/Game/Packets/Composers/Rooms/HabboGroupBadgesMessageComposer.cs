namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class HabboGroupBadgesMessageComposer : MessageComposer
    {
        public HabboGroupBadgesMessageComposer()
            : base(309)
        {
            AppendVL64(0);
        }
    }
}
