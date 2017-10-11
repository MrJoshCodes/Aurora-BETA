using AuroraEmu.Game.Items.Models;

namespace AuroraEmu.Network.Game.Packets.Composers.Items
{
    class ItemAddMessageComposer : MessageComposer
    {
        public ItemAddMessageComposer(Item item)
            : base(83)
        {
            AppendString(item.Id.ToString());
            AppendVL64(item.Definition.SpriteId);
            AppendString(item.Wallposition);
            AppendString(item.Data);
        }
    }
}
