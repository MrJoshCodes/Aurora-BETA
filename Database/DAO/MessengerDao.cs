using System.Collections.Generic;
using System.Data;
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
            DataTable result;

            using (DatabaseConnection dbClient = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbClient.Open();
                dbClient.SetQuery(
                    "SELECT id, username, figure, motto FROM players WHERE username LIKE @searchString LIMIT 30;");
                dbClient.AddParameter("@searchString", searchString + "%");

                result = dbClient.GetTable();

                dbClient.BeginTransaction();
                dbClient.Commit();
                dbClient.Dispose();
            }

            foreach (DataRow dataRow in result.Rows)
            {
                searchResult.Add(new MessengerSearch(dataRow));
            }
            
            return searchResult;
        }

        public Dictionary<int, MessengerFriend> GetFriendsById(int id, Dictionary<int, MessengerFriend> friends)
        {
            DataTable data;

            using (DatabaseConnection dbClient = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbClient.Open();
                dbClient.SetQuery(
                    "SELECT messenger_friends.user_two_id, players.username, players.figure, players.motto FROM messenger_friends LEFT JOIN players ON players.id = messenger_friends.user_two_id WHERE messenger_friends.user_one_id = @userId;");
                dbClient.AddParameter("@userId", id);

                data = dbClient.GetTable();

                dbClient.BeginTransaction();
                dbClient.Commit();
                dbClient.Dispose();
            }
            
            foreach (DataRow row in data.Rows)
            {
                friends.Add((int) row["user_two_id"], new MessengerFriend(row));
            }
            
            return friends;
        }

        public Dictionary<int, MessengerRequest> GetRequestsByPlayerId(int playerId,
            Dictionary<int, MessengerRequest> requests)
        {
            DataTable data;

            using (DatabaseConnection dbClient = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbClient.Open();
                dbClient.SetQuery("SELECT from_id, to_id FROM messenger_requests WHERE to_id = @playerId;");
                dbClient.AddParameter("@playerId", playerId);

                data = dbClient.GetTable();

                dbClient.BeginTransaction();
                dbClient.Commit();
                dbClient.Dispose();
            }

            foreach (DataRow row in data.Rows)
            {
                requests.Add((int) row["from_id"], new MessengerRequest(row));
            }

            return requests;
        }

        public void CreateFriendship(Player player, int userTwo)
        {
            using (DatabaseConnection dbClient = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbClient.Open();
                dbClient.SetQuery(
                    "INSERT INTO messenger_friends (user_one_id, user_two_id) VALUES(@userOne, @userTwo), (@userTwo, @userOne);");
                dbClient.AddParameter("@userOne", player.Id);
                dbClient.AddParameter("@userTwo", userTwo);
                dbClient.Execute();
                
                dbClient.BeginTransaction();
                dbClient.Commit();
                dbClient.Dispose();
            }
        }

        public void DestroyRequest(int userOne, int userTwo)
        {
            using (DatabaseConnection dbClient = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbClient.Open();
                dbClient.SetQuery("DELETE FROM messenger_requests WHERE to_id = @userOne AND from_id = @userTwo;");
                dbClient.AddParameter("@userOne", userOne);
                dbClient.AddParameter("@userTwo", userTwo);
                dbClient.Execute();
                
                dbClient.BeginTransaction();
                dbClient.Commit();
                dbClient.Dispose();
            }
        }

        public void DestroyAllRequests(int userOne)
        {
            using (DatabaseConnection dbClient = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbClient.Open();
                dbClient.SetQuery("DELETE FROM messenger_requests WHERE to_id = @userOne;");
                dbClient.AddParameter("@userOne", userOne);
                dbClient.Execute();
                
                dbClient.BeginTransaction();
                dbClient.Commit();
                dbClient.Dispose();
            }
        }

        public void CreateRequest(int toId, Client client)
        {
            using (DatabaseConnection dbClient = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbClient.Open();
                dbClient.SetQuery("INSERT INTO messenger_requests (to_id, from_id) VALUES (@toId, @fromId);");
                dbClient.AddParameter("@toId", toId);
                dbClient.AddParameter("@fromId", client.Player.Id);
                dbClient.Execute();
                
                dbClient.BeginTransaction();
                dbClient.Commit();
                dbClient.Dispose();
            }
        }

        public void DestroyFriendship(int userOne, int userTwo)
        {
            using (DatabaseConnection dbClient = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbClient.Open();
                dbClient.SetQuery(
                    "DELETE FROM messenger_friends WHERE user_one_id = @userOne AND user_two_id = @userTwo LIMIT 1");
                dbClient.SetQuery(
                    "DELETE FROM messenger_friends WHERE user_one_id = @userTwo AND user_two_id = @userOne LIMIT 1");
                dbClient.AddParameter("@userOne", userOne);
                dbClient.AddParameter("@userTwo", userTwo);
                dbClient.Execute();
                
                dbClient.BeginTransaction();
                dbClient.Commit();
                dbClient.Dispose();
            }
        }
    }
}