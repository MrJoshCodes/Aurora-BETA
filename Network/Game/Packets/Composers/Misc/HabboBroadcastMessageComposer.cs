using System;
using System.Collections.Generic;
using System.Text;

namespace AuroraEmu.Network.Game.Packets.Composers.Misc
{
    class HabboBroadcastMessageComposer : MessageComposer
    {
        public HabboBroadcastMessageComposer(string message)
            :base(139)
        {
            base.AppendString(message);
        }
    }
}
