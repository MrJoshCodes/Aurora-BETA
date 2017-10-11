using AuroraEmu.Game.Players.Models;

namespace AuroraEmu.Network.Game.Packets.Composers.Users
{
    class UserObjectComposer : MessageComposer
    {
        public UserObjectComposer(Player player)
            : base(5)
        {
            AppendString(player.Id.ToString());
            AppendString(player.Username);
            AppendString(player.Figure);
            AppendString(player.Gender);
            AppendString(player.Motto);
            AppendVL64(0); // no effect
            AppendString(""); // no effect
            AppendVL64(0); // no effect 
            AppendVL64(0); // ?
            AppendVL64(10); // ?
            AppendVL64(20); // ?
        }
    }
}
