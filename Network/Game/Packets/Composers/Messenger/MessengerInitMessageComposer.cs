using AuroraEmu.Game.Messenger;
using AuroraEmu.Game.Players;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Messenger
{
    public class MessengerInitMessageComposer : MessageComposer
    {
        public MessengerInitMessageComposer(Dictionary<int, MessengerFriends> friends) : base(12)
        {
            base.AppendVL64(600);
            base.AppendVL64(200);
            base.AppendVL64(600);
            base.AppendVL64(false);

            base.AppendVL64(friends.Count);
            foreach (KeyValuePair<int, MessengerFriends> friend in friends)
            {
                base.AppendVL64(friend.Value.UserTwoId);
                base.AppendString(friend.Value.Username);
                base.AppendVL64(true);
                base.AppendVL64(friend.Value.IfOnline());
                base.AppendVL64(false); //Check if in room.
                base.AppendString(friend.Value.Figure);
                base.AppendVL64(false);
                base.AppendString(friend.Value.Motto);
                base.AppendString("1970/01/29");
            }
        }
    }
}
