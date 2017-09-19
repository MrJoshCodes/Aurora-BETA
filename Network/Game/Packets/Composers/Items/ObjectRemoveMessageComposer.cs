namespace AuroraEmu.Network.Game.Packets.Composers.Items
{
    public class ObjectRemoveMessageComposer : MessageComposer
    {
        public ObjectRemoveMessageComposer(int itemId)
            : base(94)
        {
            AppendString(itemId.ToString());
            AppendString("");
            AppendVL64(false);
        }
    }
}
