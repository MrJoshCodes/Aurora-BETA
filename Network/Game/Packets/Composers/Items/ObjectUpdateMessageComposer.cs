using AuroraEmu.Game.Items;

namespace AuroraEmu.Network.Game.Packets.Composers.Items
{
    public class ObjectUpdateMessageComposer : MessageComposer
    {
        public ObjectUpdateMessageComposer(Item item)
            : base(95)
        {
            AppendVL64(item.Id);
            AppendVL64(item.DefinitionId);
            AppendVL64(item.Position.X);
            AppendVL64(item.Position.Y);
            AppendVL64(item.Rotation);
            AppendString(item.Position.Z.ToString());
            AppendVL64(0);
            AppendString(item.Data);
            AppendVL64(-1);
        }
    }
}
