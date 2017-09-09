namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
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
