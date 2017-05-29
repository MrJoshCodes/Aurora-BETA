using AuroraEmu.Network.Game.Packets;
using System.Data;

namespace AuroraEmu.Game.Messenger
{
    public class MessengerRequest
    {
        public int RequestId { get; set; }
        public int ToId { get; set; }
        public int FromId { get; set; }

        public MessengerRequest(DataRow Row)
        {
            RequestId = (int)Row["request_id"];
            ToId = (int)Row["to_id"];
            FromId = (int)Row["from_id"];
        }
        public void Serialize(MessageComposer composer)
        {
        }
    }
}
