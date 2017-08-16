using AuroraEmu.Game.Messenger;
using System.Collections.Generic;
using AuroraEmu.Game.Players;

namespace AuroraEmu.Network.Game.Packets.Composers.Messenger
{
    public class MessengerInitMessageComposer : MessageComposer
    {
        public MessengerInitMessageComposer(Dictionary<int, MessengerFriend> friends) : base(12)
        {
            AppendVL64(600);
            AppendVL64(200);
            AppendVL64(600);
            AppendVL64(0);

            AppendVL64(friends.Count);
            foreach (MessengerFriend friend in friends.Values)
            {
                AppendVL64(friend.UserTwoId);
                AppendString(friend.Username);
                AppendVL64(1);
                AppendVL64(Engine.MainDI.ClientController.TryGetPlayer(friend.UserTwoId, out Player player));
                AppendVL64(true);
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
