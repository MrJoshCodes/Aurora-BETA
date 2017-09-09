using AuroraEmu.Game.Items;

namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    public class ObjectAddMessageComposer : MessageComposer
    {
        public ObjectAddMessageComposer(Item item)
            : base(93)
        {
            AppendVL64(item.Id);
            AppendVL64(item.Definition.SpriteId);
            AppendVL64(item.X);
            AppendVL64(item.Y);
            AppendVL64(item.Rotation);
            AppendString(item.Z.ToString().Replace(',', '.'));
            AppendVL64(0);
            AppendString(item.Data);
            AppendVL64(-1);
        }
    }
}
