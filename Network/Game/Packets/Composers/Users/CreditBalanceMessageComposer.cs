namespace AuroraEmu.Network.Game.Packets.Composers.Users
{
    public class CreditBalanceMessageComposer : MessageComposer
    {
        public CreditBalanceMessageComposer(int coins) : base(6)
        {
            AppendString($"{coins}.0");
        }
    }
}
