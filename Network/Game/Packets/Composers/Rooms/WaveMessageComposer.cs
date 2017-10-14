using System;
using System.Collections.Generic;
using System.Text;

namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class WaveMessageComposer : MessageComposer
    {
        public WaveMessageComposer(int virtualId)
        : base(481)
        {
            base.AppendVL64(virtualId);
        }
    }
}
