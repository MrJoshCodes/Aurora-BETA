using AuroraEmu.Game.Groups.Models;

namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    public class HabboGroupDetailsMessageComposer : MessageComposer
    {
        public HabboGroupDetailsMessageComposer(Group group) : base(311)
        {
            AppendVL64(group.Id);
            AppendString(group.Name);
            AppendString(group.Description);
            AppendVL64(1);
            AppendString("noidea");
        }
    }
}