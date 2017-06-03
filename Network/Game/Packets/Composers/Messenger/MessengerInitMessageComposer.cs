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
            AppendVL64(0);

            AppendVL64(friends.Count);
            foreach (MessengerFriends friend in friends.Values)
            {
                AppendVL64(friend.UserTwoId);
                AppendString(friend.Username);
                AppendVL64(1);
                AppendVL64(ClientManager.GetInstance().PlayerIsOnline(friend.UserTwoId));
                AppendVL64(false); //Check if in room.
                AppendString(friend.Figure);
                AppendVL64(0);
                AppendString(friend.Motto);
                AppendString("");
            }

            AppendVL64(0);
            AppendVL64(0);
        }
    }
}
