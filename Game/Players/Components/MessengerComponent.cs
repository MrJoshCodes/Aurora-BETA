using System.Collections.Generic;
using AuroraEmu.Game.Messenger;
using AuroraEmu.Network.Game.Packets;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;
using System;

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

        public Dictionary<int, MessengerFriend> GetFriends()
        {
            return Engine.MainDI.MessengerDao.GetFriendsById(Player.Id, Friends);
        }

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

        public Dictionary<int, MessengerRequest> GetRequests()
        {
            return Engine.MainDI.MessengerDao.GetRequestsByPlayerId(Player.Id, Requests);
        }

        public bool IsFriends(int userTwoId)
        {
            return Friends.ContainsKey(userTwoId);
        }

        public void Dispose()
        {
            Friends.Clear();
            Requests.Clear();
            GC.SuppressFinalize(this);
        }
    }
}