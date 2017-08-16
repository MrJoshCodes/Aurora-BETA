using AuroraEmu.DI.Game.Messenger;
using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;
using System.Collections.Generic;

namespace AuroraEmu.Game.Messenger
{
    public class MessengerController : IMessengerController
    {
        public MessageComposer MessengerSearch(string searchString, Client client)
        {
            List<MessengerSearch> friends = new List<MessengerSearch>();
            List<MessengerSearch> notFriends = new List<MessengerSearch>();

            foreach (MessengerSearch searchResult in SearchForUsers(searchString))
            {
                if (client.Player.MessengerComponent.IsFriends(searchResult.Id))
                {
                    friends.Add(searchResult);
                }
                else
                {
                    notFriends.Add(searchResult);
                }
            }
            return new HabboSearchResultMessageComposer(friends, notFriends);
        }

        public List<MessengerSearch> SearchForUsers(string searchString)
        {
            return Engine.MainDI.MessengerDao.SearchForUsers(searchString);
        }
    }
}