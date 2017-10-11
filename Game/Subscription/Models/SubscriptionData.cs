using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Subscription.Models
{
    public class SubscriptionData
    {
        public string Subscription { get; set; }
        public int TimeBought { get; set; }
        public int TimeExpire { get; set; }

        public SubscriptionData(MySqlDataReader reader)
        {
            Subscription = reader.GetString("subscription_id");
            TimeBought = reader.GetInt32("timestamp_bought");
            TimeExpire = reader.GetInt32("timestamp_expire");
        }
    }
}
