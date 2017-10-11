using AuroraEmu.Game.Players.Models;

namespace AuroraEmu.Network.Game.Packets.Composers.Moderation
{
    public class ModeratorUserInfoComposer : MessageComposer
    {
        public ModeratorUserInfoComposer(Player player, bool online)
            : base(1000)
        {
            AppendVL64(player.Id);
            AppendString(player.Username);
            AppendVL64(1); // ?
            AppendVL64(5); // Last login?
            AppendVL64(online); // Online
            AppendVL64(0); // CFHs
            AppendVL64(0); // Abusive CFHs
            AppendVL64(0); // Cautions
            AppendVL64(0); // Bans
        }
    }
}