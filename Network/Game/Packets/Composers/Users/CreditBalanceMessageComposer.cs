using AuroraEmu.Game.Clients;

namespace AuroraEmu.Network.Game.Packets.Composers.Users
{
    public class CreditBalanceMessageComposer : MessageComposer
    {
        public CreditBalanceMessageComposer(Client client) : base(6)
        {
            AppendString($"{client.Player.Coins}.0");
        }
    }
}
