namespace AuroraEmu.Network.Game.Packets.Composers.Messenger
{
    public class NewConsoleMessageComposer : MessageComposer
    {
        public NewConsoleMessageComposer(string message, int conversationId)
            : base(134)
        {
            AppendVL64(conversationId);
            AppendString(message);
        }
    }
}
