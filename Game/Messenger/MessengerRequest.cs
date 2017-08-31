using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Messenger
{
    public class MessengerRequest
    {
        public int FromId { get; set; }
        public int ToId { get; set; }

        public MessengerRequest(MySqlDataReader reader)
        {
            FromId = reader.GetInt32("from_id");
            ToId = reader.GetInt32("to_id");
        }

        public MessengerRequest(int fromId, int toId)
        {
            FromId = fromId;
            ToId = toId;
        }
    }
}