using AuroraEmu.Game.Items.Models;

namespace AuroraEmu.Network.Game.Packets.Composers.Catalogue
{
    public class PresentOpenedMessageComposer : MessageComposer
    {
        public PresentOpenedMessageComposer(ItemDefinition definition) : base(129)
        {
            AppendString(definition.SpriteType);
            AppendVL64(definition.SpriteId);
            AppendString(definition.SwfName);
        }
    }
}