using AuroraEmu.Game.Items.Models;

namespace AuroraEmu.Network.Game.Packets.Composers.Items
{
    public class ItemUpdateMessageComposer : MessageComposer
    {
        public ItemUpdateMessageComposer(Item item) : base(85)
        {
            AppendString(item.Id.ToString());
            AppendVL64(item.Definition.SpriteId);
            AppendString(item.Wallposition);
            AppendString(item.Data);
        }
    }
}
