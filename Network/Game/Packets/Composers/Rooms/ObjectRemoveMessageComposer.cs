namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    public class ObjectRemoveMessageComposer : MessageComposer
    {
        public ObjectRemoveMessageComposer(int itemId)
            : base(94)
        {
            AppendVL64(itemId);
            AppendString("");
            AppendVL64(false);
        }
    }
}
