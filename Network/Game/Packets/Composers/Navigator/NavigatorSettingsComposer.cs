namespace AuroraEmu.Network.Game.Packets.Composers.Navigator
{
    public class NavigatorSettingsComposer : MessageComposer
    {
        public NavigatorSettingsComposer(int homeRoom) : base(455)
        {
            AppendVL64(homeRoom);
        }
    }
}