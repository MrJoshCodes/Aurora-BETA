namespace AuroraEmu.Network.Game.Packets.Composers.Users.Clothing
{
    using AuroraEmu.Game.Clients;

    public class WardrobeMessageComposer : MessageComposer
    {
        public WardrobeMessageComposer(Client client)
            : base(267)
        {
            AppendVL64(true);
            AppendVL64(1);
            AppendString(client.Player.Username);
            AppendString(client.Player.Gender);

        }
    }
}
