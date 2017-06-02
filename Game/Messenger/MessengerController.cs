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

        public static MessengerController GetInstance()
        {
            if (messengerControllerInstance == null)
                messengerControllerInstance = new MessengerController();
            return messengerControllerInstance;
        }
    }
}
