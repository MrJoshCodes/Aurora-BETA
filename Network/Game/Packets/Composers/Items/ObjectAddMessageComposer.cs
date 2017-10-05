using AuroraEmu.Game.Items;

namespace AuroraEmu.Network.Game.Packets.Composers.Items
{
    public class ObjectAddMessageComposer : MessageComposer
    {
        public ObjectAddMessageComposer(Item item)
            : base(93)
        {
            AppendVL64(item.Id);
            AppendVL64(item.Definition.SpriteId);
            AppendVL64(item.Position.X);
            AppendVL64(item.Position.Y);
            AppendVL64(item.Rotation);
            AppendString(item.Position.Z.ToString().Replace(',', '.'));
            AppendVL64(0);
            AppendString(item.Data);
            AppendVL64(-1);
        }
    }
}
