using System.Data;

namespace AuroraEmu.Game.Messenger
{
    public class MessengerRequest
    {
        public int FromId { get; set; }
        public int ToId { get; set; }

        public MessengerRequest(DataRow row)
        {
            FromId = (int) row["from_id"];
            ToId = (int) row["to_id"];
        }

        public MessengerRequest(int fromId, int toId)
        {
            FromId = fromId;
            ToId = toId;
        }
    }
}