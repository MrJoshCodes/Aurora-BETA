using AuroraEmu.Game.Clients;
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
            AppendVL64(search.Id);
            AppendString(search.Username);
            AppendString(search.Motto);
            AppendVL64(ClientManager.GetInstance().PlayerIsOnline(search.Id)); // is online
            AppendVL64(false);
            AppendString("");
            AppendVL64(0);
            AppendString(search.Figure);
            AppendString("1970-01-01");
        }
    }
}
