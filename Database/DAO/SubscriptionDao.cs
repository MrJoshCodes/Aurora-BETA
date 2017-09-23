using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Subscription;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace AuroraEmu.Database.DAO
{
    public class SubscriptionDao : ISubscriptionDao
    {
        public void GetSubscriptionData(Dictionary<string, SubscriptionData> data, int userId)
        {
            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT * FROM player_subscriptions WHERE user_id = @userId;");
                dbConnection.AddParameter("@userId", userId);

                using (MySqlDataReader reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                        data.Add(reader.GetString("subscription_id"), new SubscriptionData(reader));
            }
        }
    }
}
