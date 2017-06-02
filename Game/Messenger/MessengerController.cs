using AuroraEmu.Database;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Players;
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

        public MessageComposer MessengerSearch(string searchString, Client client)
        {
            List<MessengerSearch> friends = new List<MessengerSearch>();
            List<MessengerSearch> notFriends = new List<MessengerSearch>();

            foreach(MessengerSearch searchResult in SearchForUsers(searchString))
            {
                if(CheckIfFriends(client.Player.Id, searchResult.Id))
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

                foreach(DataRow Row in result.Rows)
                {
                    searchResult.Add(new MessengerSearch(Row));
                }
            }
            return searchResult;
        }

        public bool CheckIfFriends(int userOne, int userTwo)
        {
            using(DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("SELECT * FROM messenger_friends WHERE (user_one_id = @userOne AND user_two_id = @userTwo) OR (user_two_id = @userOne AND user_one_id = @userTwo);");
                dbClient.AddParameter("@userOne", userOne);
                dbClient.AddParameter("@userTwo", userTwo);

                dbClient.Open();

                if(dbClient.GetRow() != null)
                {
                    return true;
                }
            }
            return false;
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

                foreach(DataRow Row in data.Rows)
                {
                    friends.Add((int)Row["user_two_id"], new MessengerFriends(Row));
                }
            }
            return friends;
        }

        public MessengerFriends GetFriendById(int id)
        {
            MessengerFriends friend = null;
            if (!friends.TryGetValue(id, out friend))
                return null;

            return friend;
        }

        public static MessengerController GetInstance()
        {
            if (messengerControllerInstance == null)
                messengerControllerInstance = new MessengerController();
            return messengerControllerInstance;
        }
    }
}
