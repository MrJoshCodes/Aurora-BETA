namespace AuroraEmu.Network.Game.Packets.Composers.Items
{
    public class ObjectDataUpdateMessageComposer : MessageComposer
    {
        public ObjectDataUpdateMessageComposer(int itemId, string extraData) : base(88)
        {
            AppendString(itemId + "");
            AppendString(extraData);
        }
    }
}
