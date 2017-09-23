using System.Collections.Generic;
using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Messenger;
using AuroraEmu.Game.Players;

namespace AuroraEmu.Database.DAO
{
    public class MessengerDao : IMessengerDao
    {
        public List<MessengerSearch> SearchForUsers(string searchString)
        {
            List<MessengerSearch> searchResult = new List<MessengerSearch>();

            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery(
                    "SELECT id, username, figure, motto FROM players WHERE username LIKE @searchString LIMIT 30;");
                dbConnection.AddParameter("@searchString", searchString + "%");
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                        searchResult.Add(new MessengerSearch(reader));
            }

            return searchResult;
        }

        public Dictionary<int, MessengerFriend> GetFriendsById(int id, Dictionary<int, MessengerFriend> friends)
        {
            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery(
                    "SELECT messenger_friends.user_two_id, players.username, players.figure, players.motto FROM messenger_friends LEFT JOIN players ON players.id = messenger_friends.user_two_id WHERE messenger_friends.user_one_id = @userId;");
                dbConnection.AddParameter("@userId", id);
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                        friends.Add(reader.GetInt32("user_two_id"), new MessengerFriend(reader));
            }

            return friends;
        }

        public Dictionary<int, MessengerRequest> GetRequestsByPlayerId(int playerId,
            Dictionary<int, MessengerRequest> requests)
        {
            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT from_id, to_id FROM messenger_requests WHERE to_id = @playerId;");
                dbConnection.AddParameter("@playerId", playerId);

                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                        requests.Add(reader.GetInt32("from_id"), new MessengerRequest(reader));
            }

            return requests;
        }

        public void CreateFriendship(Player player, int userTwo)
        {
            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery(
                    "INSERT INTO messenger_friends (user_one_id, user_two_id) VALUES(@userOne, @userTwo), (@userTwo, @userOne);");
                dbConnection.AddParameter("@userOne", player.Id);
                dbConnection.AddParameter("@userTwo", userTwo);
                dbConnection.Execute();
            }
        }

        public void DestroyRequest(int userOne, int userTwo)
        {
            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("DELETE FROM messenger_requests WHERE to_id = @userOne AND from_id = @userTwo;");
                dbConnection.AddParameter("@userOne", userOne);
                dbConnection.AddParameter("@userTwo", userTwo);
                dbConnection.Execute();
            }
        }

        public void DestroyAllRequests(int userOne)
        {
            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("DELETE FROM messenger_requests WHERE to_id = @userOne;");
                dbConnection.AddParameter("@userOne", userOne);
                dbConnection.Execute();
            }
        }

        public void CreateRequest(int toId, Client client)
        {
            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("INSERT INTO messenger_requests (to_id, from_id) VALUES (@toId, @fromId);");
                dbConnection.AddParameter("@toId", toId);
                dbConnection.AddParameter("@fromId", client.Player.Id);
                dbConnection.Execute();
            }
        }

        public void DestroyFriendship(int userOne, int userTwo)
        {
            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery(
                    "DELETE FROM messenger_friends WHERE user_one_id = @userOne AND user_two_id = @userTwo LIMIT 1");
                dbConnection.SetQuery(
                    "DELETE FROM messenger_friends WHERE user_one_id = @userTwo AND user_two_id = @userOne LIMIT 1");
                dbConnection.AddParameter("@userOne", userOne);
                dbConnection.AddParameter("@userTwo", userTwo);
                dbConnection.Execute();
            }
        }
    }
}