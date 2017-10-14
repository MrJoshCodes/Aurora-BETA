using AuroraEmu.Game.Players.Models;

namespace AuroraEmu.Network.Game.Packets.Composers.Users
{
    public class UserChangeMessageComposer : MessageComposer
    {
        public UserChangeMessageComposer(Player player)
            : base(266)
        {
            AppendVL64(-1);
            AppendString(player.Figure);
            AppendString(player.Gender.ToLower());
            AppendString(player.Motto);
        }

        public UserChangeMessageComposer(int virtualId, Player player)
            : base(266)
        {
            AppendVL64(virtualId);
            AppendString(player.Figure);
            AppendString(player.Gender.ToLower());
            AppendString(player.Motto);
        }
    }
}
