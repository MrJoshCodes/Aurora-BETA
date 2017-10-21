namespace AuroraEmu.Network.Game.Packets.Composers.Moderation
{
    public class ModMessageComposer : MessageComposer
    {
        public ModMessageComposer(string text, string url = "")
            : base(161)
        {
            AppendString(text);
            AppendString(url);
        }
    }
}