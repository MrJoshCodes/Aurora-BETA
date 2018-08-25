using System.Collections.Generic;
using System;
using AuroraEmu.Game.Messenger.Models;
using AuroraEmu.Game.Players.Models;
using AuroraEmu.Network.Game.Packets;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;
using System.Threading.Tasks;
using AuroraEmu.Game.Clients;

namespace AuroraEmu.Game.Players.Components
{
    public class MessengerComponent : IDisposable
    {
        private Player Player { get; }
        public Dictionary<int, MessengerFriend> Friends { get; }
        public Dictionary<int, MessengerRequest> Requests { get; }

        public MessengerComponent(Player player)
        {
            Player = player;
            Friends = new Dictionary<int, MessengerFriend>();
            Requests = new Dictionary<int, MessengerRequest>();
            GetFriends();
            GetRequests();
        }

        public Dictionary<int, MessengerFriend> GetFriends() =>
            Engine.Locator.MessengerController.Dao.GetFriendsById(Player.Id, Friends);

        public Dictionary<int, MessengerRequest> GetRequests() =>
            Engine.Locator.MessengerController.Dao.GetRequestsByPlayerId(Player.Id, Requests);

        public bool IsFriends(int userTwoId) =>
            Friends.ContainsKey(userTwoId);

        public void AddFriend(int id, MessengerFriend friend)
        {
            if (!Friends.ContainsKey(id))
                Friends.Add(id, friend);
        }

        public void RemoveFriend(int id)
        {
            if (Friends.ContainsKey(id))
                Friends.Remove(id);
        }

        public void AddRequest(int id, MessengerRequest request)
        {
            if (!Requests.ContainsKey(id))
                Requests.Add(id, request);
        }

        public MessengerRequest GetRequest(int id)
        {
            if (Requests.TryGetValue(id, out MessengerRequest request) && request != null)
                return request;
            return null;
        }

        public void Dispose()
        {
            Friends.Clear();
            Requests.Clear();
        }

        public void SendUpdate()
        {
            FriendListUpdateMessageComposer friendListUpdateMessageComposer = new FriendListUpdateMessageComposer(Player);

            Parallel.ForEach(Friends.Values, (friend) =>
            {
                Client client = Engine.Locator.ClientController.GetClientByHabbo(friend.UserTwoId);
                
                if (client != null)
                {
                    client.SendComposer(friendListUpdateMessageComposer);
                }
            });
        }
    }
}