using AuroraEmu.Network.Game.Packets;
using System.Data;

namespace AuroraEmu.Game.Messenger
{
    public class MessengerRequest
    {
        public int FromId { get; set; }
        public int ToId { get; set; }

        public MessengerRequest(DataRow Row)
        {
            FromId = (int)Row["from_id"];
            ToId = (int)Row["to_id"];
        }
    }
}
