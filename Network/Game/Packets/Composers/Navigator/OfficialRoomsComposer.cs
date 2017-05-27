using AuroraEmu.Game.Navigator;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Navigator
{
    class OfficialRoomsComposer : MessageComposer
    {
        public OfficialRoomsComposer(IList<FrontpageItem> frontpageItems)
            : base(450)
        {
            AppendVL64(0);
            AppendVL64(frontpageItems.Count);

            foreach (FrontpageItem item in frontpageItems)
            {
                AppendString(item.Name);
                AppendString(item.Description);
                AppendVL64(item.Size);
                AppendString(item.Name);
                AppendString(item.Image);
                AppendVL64(0);
                AppendVL64(item.Type);

                if (item.Type == 1)
                    AppendString(item.Tag);
            }
        }
    }
}