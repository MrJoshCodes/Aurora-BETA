namespace AuroraEmu.Network.Game.Packets.Composers.Handshake
{
    class InitCryptoMessageComposer : MessageComposer
    {
        public InitCryptoMessageComposer()
            : base(277)
        {
            AppendVL64(0);
        }

        public InitCryptoMessageComposer(string token)
            :base(277)
        {
            AppendString(token);
            AppendVL64(0);
        }
    }
}
