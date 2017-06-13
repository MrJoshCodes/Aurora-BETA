using AuroraEmu.Database;
using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;
using System.Collections.Generic;
using System.Data;

namespace AuroraEmu.Game.Messenger
{
    public class MessengerController
    {
        private static MessengerController messengerControllerInstance;
        private Dictionary<int, MessengerFriends> friends;
        private Dictionary<int, MessengerRequest> requests;

        public MessageComposer MessengerSearch(string searchString, Client client)
        {
            List<MessengerSearch> friends = new List<MessengerSearch>();
            List<MessengerSearch> notFriends = new List<MessengerSearch>();

            foreach(MessengerSearch searchResult in SearchForUsers(searchString))
            {
                if(IsFriends(client, searchResult.Id))
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
            List<MessengerSearch> searchResult = new List<MessengerSearch>();
            DataTable result = null;

            using (DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("SELECT id, username, figure, motto FROM players WHERE username LIKE @searchString LIMIT 30;");
                dbClient.AddParameter("@searchString", searchString + "%");
                dbClient.Open();

                result = dbClient.GetTable();

                foreach(DataRow row in result.Rows)
                {
                    searchResult.Add(new MessengerSearch(row));
                }
            }
            return searchResult;
        }

        public bool IsFriends(Client client, int userTwoID)
        {
            return client.Friends.TryGetValue(userTwoID, out MessengerFriends friend);
        }

        public Dictionary<int, MessengerFriends> GetFriendsById(int id)
        {
            friends = new Dictionary<int, MessengerFriends>();
            DataTable data = null;

            using(DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("SELECT messenger_friends.user_two_id, players.username, players.figure, players.motto FROM messenger_friends LEFT JOIN players ON players.id = messenger_friends.user_two_id WHERE messenger_friends.user_one_id = @userId;");
                dbClient.AddParameter("@userId", id);
                dbClient.Open();

                data = dbClient.GetTable();

                foreach(DataRow row in data.Rows)
                {
                    friends.Add((int)row["user_two_id"], new MessengerFriends(row));
                }
            }
            return friends;
        }

        public MessengerFriends GetFriendById(int id)
        {
            if (!friends.TryGetValue(id, out MessengerFriends friend))
                return null;

            return friend;
        }

        public Dictionary<int, MessengerRequest> GetRequestsByPlayerId(int playerId)
        {
            requests = new Dictionary<int, MessengerRequest>();

            DataTable data = null;

            using(DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("SELECT from_id, to_id FROM messenger_requests WHERE to_id = @playerId;");
                dbClient.AddParameter("@playerId", playerId);
                dbClient.Open();

                data = dbClient.GetTable();

                foreach(DataRow row in data.Rows)
                {
                    requests.Add((int)row["from_id"], new MessengerRequest(row));
                }
            }
            return requests;
        }

        public void CreateFriendship(int userOne, int userTwo)
        {
            using (DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("INSERT INTO messenger_friends (user_one_id, user_two_id) VALUES(@userOne, @userTwo), (@userTwo, @userOne);");
                dbClient.AddParameter("@userOne", userOne);
                dbClient.AddParameter("@userTwo", userTwo);
                dbClient.Open();
                dbClient.Execute();
            }
            DestroyRequest(userOne, userTwo);
        }

        public void DestroyRequest(int userOne, int userTwo)
        {
            if (requests.TryGetValue(userTwo, out MessengerRequest request))
                requests.Remove(userTwo);
            using (DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("DELETE FROM messenger_requests WHERE to_id = @userOne AND from_id = @userTwo;");
                dbClient.SetQuery("DELETE FROM messenger_requests WHERE to_id = @userTwo AND from_id = @userOne;");
                dbClient.AddParameter("@userOne", userOne);
                dbClient.AddParameter("@userTwo", userTwo);
                dbClient.Open();
                dbClient.Execute();
            }
        }

        public void DestroyAllRequests(int userOne)
        {
            using (DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("DELETE FROM messenger_requests WHERE to_id = @userOne;");
                dbClient.AddParameter("@userOne", userOne);
                dbClient.Open();
                dbClient.Execute();
            }
        }

        public void CreateRequest(int toId, Client client)
        {
            requests = new Dictionary<int, MessengerRequest>();
            using (DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("INSERT INTO messenger_requests (to_id, from_id) VALUES (@toId, @fromId);");
                dbClient.AddParameter("@toId", toId);
                dbClient.AddParameter("@fromId", client.Player.Id);
                dbClient.Open();
                dbClient.Execute();
            }

            DataRow row = null;

            using (DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("SELECT to_id, from_id FROM messenger_requests WHERE to_id = @toId AND from_id = @fromId;");
                dbClient.AddParameter("@toId", toId);
                dbClient.AddParameter("@fromId", client.Player.Id);
                dbClient.Open();

                row = dbClient.GetRow();
            }
            requests.Add(client.Player.Id, new MessengerRequest(row));
        }

        public MessageComposer UpdateFriendlist(int userId)
        {
            GetFriendsById(userId);

            Dictionary<int, MessengerFriends> updatedFriends = new Dictionary<int, MessengerFriends>();
            int updateCount = 0;

            lock (friends)
            {
                foreach (MessengerFriends friend in friends.Values)
                {
                    updateCount++;
                    updatedFriends.Add(friend.UserTwoId, friend);
                }
            }

            return new FriendListUpdateMessageComposer(updateCount, updatedFriends);
        }

        public MessengerRequest GetRequest(int fromId, int toId)
        {
            if (requests.TryGetValue(fromId, out MessengerRequest request))
                return request;

            DataRow row = null;

            using (DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("SELECT to_id, from_id FROM messenger_requests WHERE to_id = @toId AND from_id = @fromId LIMIT 1;");
                dbClient.AddParameter("@toId", toId);
                dbClient.AddParameter("@fromId", fromId);
                dbClient.Open();

                row = dbClient.GetRow();
            }

            return new MessengerRequest(row);
        }

        public void DestroyFriendship(int userOne, int userTwo)
        {
            using (DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("DELETE FROM messenger_friends WHERE user_one_id = @userOne AND user_two_id = @userTwo LIMIT 1");
                dbClient.SetQuery("DELETE FROM messenger_friends WHERE user_one_id = @userTwo AND user_two_id = @userOne LIMIT 1");
                dbClient.AddParameter("@userOne", userOne);
                dbClient.AddParameter("@userTwo", userTwo);
                dbClient.Open();
                dbClient.Execute();
            }
        }

        public static MessengerController GetInstance()
        {
            if (messengerControllerInstance == null)
                messengerControllerInstance = new MessengerController();
            return messengerControllerInstance;
        }
    }
}
