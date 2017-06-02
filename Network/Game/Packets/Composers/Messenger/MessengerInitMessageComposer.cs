using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Messenger;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Messenger
{
    public class MessengerInitMessageComposer : MessageComposer
    {
        public MessengerInitMessageComposer(Dictionary<int, MessengerFriends> friends) : base(12)
        {
            AppendVL64(600);
            AppendVL64(200);
            AppendVL64(600);
            AppendVL64(false);

            AppendVL64(friends.Count);
            foreach (KeyValuePair<int, MessengerFriends> friend in friends)
            {
                AppendVL64(friend.Value.UserTwoId);
                AppendString(friend.Value.Username);
                AppendVL64(true);
                AppendVL64(ClientManager.GetInstance().PlayerIsOnline(friend.Value.UserTwoId));
                AppendVL64(false); //Check if in room.
                AppendString(friend.Value.Figure);
                AppendVL64(false);
                AppendString(friend.Value.Motto);
                AppendString("1970/01/29");
            }
        }
    }
}
