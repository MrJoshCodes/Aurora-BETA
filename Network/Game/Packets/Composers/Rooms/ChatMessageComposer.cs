namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class ChatMessageComposer : MessageComposer
    {
        public ChatMessageComposer(int virtualId, string input)
            : base(24)
        {
            AppendVL64(virtualId);
            AppendString(input);
            AppendVL64(0);
        }
    }
}
