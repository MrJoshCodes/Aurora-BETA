using AuroraEmu.Database;
using AuroraEmu.DI.Game.Subscription;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace AuroraEmu.Game.Subscription
{
    public class SubscriptionController : ISubscriptionController
    {
        public Dictionary<string, SubscriptionData> GetSubscriptionData(Dictionary<string, SubscriptionData> data, int userId)
        {
            using(DatabaseConnection dbClient = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbClient.SetQuery("SELECT * FROM player_subscriptions WHERE user_id = @userId;");
                dbClient.AddParameter("@userId", userId);

                using (MySqlDataReader reader = dbClient.ExecuteReader())
                    while (reader.Read())
                        data.Add(reader.GetString("subscription_id"), new SubscriptionData(reader));
            }

            return data;
        }
    }
}
