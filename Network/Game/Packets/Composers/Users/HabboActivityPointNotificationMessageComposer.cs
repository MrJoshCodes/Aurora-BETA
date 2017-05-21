using AuroraEmu.Game.Clients;

namespace AuroraEmu.Network.Game.Packets.Composers.Users
{
    public class HabboActivityPointNotificationMessageComposer : MessageComposer
    {
        public HabboActivityPointNotificationMessageComposer(Client client) : base(438)
        {
            AppendVL64(client.Player.Pixels);
            AppendVL64(0);
        }
    }
}
