using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraEmu.Network.Game.Packets.Composers.Messenger
{
    public class MessengerErrorMessageComposer : MessageComposer
    {
        public MessengerErrorMessageComposer()
            : base(260)
        {
            AppendVL64(39);
            AppendVL64(3);
        }
    }
}
