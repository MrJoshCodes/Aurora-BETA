using AuroraEmu.Game.Messenger;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Messenger
{
    public class NewBuddyRequestMessageComposer : MessageComposer
    {
        public NewBuddyRequestMessageComposer(MessengerRequest request, string username)
            : base(132)
        {
            AppendVL64(request.FromId);
            AppendString(username);
            AppendString(request.FromId.ToString());
        }
    }
}
