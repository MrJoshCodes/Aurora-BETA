using AuroraEmu.Game.Messenger;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Messenger
{
    public class HabboSearchResultMessageComposer : MessageComposer
    {
        public HabboSearchResultMessageComposer(List<MessengerSearch> friends, List<MessengerSearch> notFriends)
            : base(435)
        {
            AppendVL64(friends.Count);
            foreach(MessengerSearch search in friends)
            {
                Serialize(search);
            }

            AppendVL64(notFriends.Count);
            foreach(MessengerSearch search in notFriends)
            {
                Serialize(search);
            }
        }
        public void Serialize(MessengerSearch search)
        {
            base.AppendVL64(search.Id);
            base.AppendString(search.Username);
            base.AppendString(search.Motto);
            base.AppendVL64(true); // is online
            base.AppendVL64(false);
            base.AppendString("");
            base.AppendVL64(0);
            base.AppendString(search.Figure);
            base.AppendString("1970-01-01");
        }
    }
}
